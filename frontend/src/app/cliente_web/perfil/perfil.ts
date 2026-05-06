import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../auth/auth.service';
import { ApiService } from '../../core/api.service';

@Component({
  selector: 'app-perfil',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './perfil.html',
  styleUrls: ['./perfil.css']
})
export class PerfilComponent implements OnInit {

  perfil: any = {
    idTipoDocumento: 1,
    numeroDocumento: '',
    nombre: '',
    apellido: '',
    telefono: '',
    correo: '',
    direccion: ''
  };

  tiposDocumento: any[] = [];
  cargando = false;
  consultandoDni = false;
  mensaje = '';
  error = '';

  constructor(
    private auth: AuthService,
    private api: ApiService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const usuario = this.auth.getUsuarioActual();
    if (usuario) {
      this.perfil.nombre = usuario.nombre ?? '';
      this.perfil.apellido = (usuario as any).apellido ?? '';
      this.perfil.telefono = (usuario as any).telefono ?? '';
      this.perfil.correo = (usuario as any).correo ?? usuario.correoUsuario ?? '';
    }

    this.api.getTiposDocumento().subscribe({
      next: (data) => { this.tiposDocumento = data ?? []; },
      error: () => {}
    });

    // Cargar datos completos desde el API
    this.api.get<any>('perfil').subscribe({
      next: (data) => {
        if (data) {
          this.perfil = {
            idTipoDocumento: data.id_tipo_documento ?? this.perfil.idTipoDocumento ?? 1,
            numeroDocumento: data.numero_documento_cliente ?? '',
            nombre: data.nombre_cliente ?? data.nombre_usuario ?? this.perfil.nombre,
            apellido: data.apellido_cliente ?? this.perfil.apellido,
            telefono: data.telefono_cliente ?? this.perfil.telefono,
            correo: data.correo_usuario ?? this.perfil.correo,
            direccion: data.direccion_cliente ?? ''
          };
        }
      },
      error: () => {}
    });
  }

  get esDni(): boolean {
    const tipo = this.tiposDocumento.find(t => t.id_tipo_documento === Number(this.perfil.idTipoDocumento));
    const nombre = (tipo?.nombre_tipo_documento ?? '').toString().toLowerCase();
    return nombre.includes('dni');
  }

  consultarDni(): void {
    this.mensaje = '';
    this.error = '';

    const dni = (this.perfil.numeroDocumento ?? '').trim();

    if (!/^\d{8}$/.test(dni)) {
      this.error = 'El DNI debe tener 8 dígitos.';
      return;
    }

    this.consultandoDni = true;

    this.api.consultarDni(dni).subscribe({
      next: (data) => {
        const nombres = data?.nombres ?? data?.nombre ?? '';
        const apellidoP = data?.apellido_paterno ?? '';
        const apellidoM = data?.apellido_materno ?? '';
        const nombreCompleto = data?.nombre_completo ?? '';

        if (!nombres && nombreCompleto) {
          const partes = nombreCompleto.trim().split(/\s+/);
          if (partes.length >= 2) {
            this.perfil.apellido = partes.slice(0, 2).join(' ');
            this.perfil.nombre = partes.slice(2).join(' ') || this.perfil.nombre;
          } else {
            this.perfil.nombre = nombreCompleto;
          }
        } else {
          this.perfil.nombre = nombres || this.perfil.nombre;
          this.perfil.apellido = [apellidoP, apellidoM].filter(Boolean).join(' ') || this.perfil.apellido;
        }

        this.consultandoDni = false;
      },
      error: () => {
        this.error = 'No se pudo consultar el DNI.';
        this.consultandoDni = false;
      }
    });
  }

  guardar(): void {
    this.mensaje = '';
    this.error = '';

    if (!this.perfil.nombre.trim()) { this.error = 'El nombre es obligatorio.'; return; }
    if (!this.perfil.apellido.trim()) { this.error = 'El apellido es obligatorio.'; return; }
    if (this.esDni && this.perfil.numeroDocumento && !/^\d{8}$/.test(this.perfil.numeroDocumento.trim())) {
      this.error = 'El DNI debe tener 8 dígitos.';
      return;
    }
    if (this.perfil.telefono && !/^9\d{8}$/.test(this.perfil.telefono.trim())) {
      this.error = 'Celular inválido (9 dígitos, empieza con 9).'; return;
    }

    this.cargando = true;
    this.api.put<any>('perfil', {
      idTipoDocumento: Number(this.perfil.idTipoDocumento) || 1,
      numeroDocumentoCliente: this.perfil.numeroDocumento.trim() || null,
      nombreCliente: this.perfil.nombre.trim(),
      apellidoCliente: this.perfil.apellido.trim(),
      telefonoCliente: this.perfil.telefono.trim() || null,
      direccionCliente: this.perfil.direccion.trim() || null
    }).subscribe({
      next: () => {
        this.cargando = false;
        this.mensaje = 'Perfil actualizado correctamente.';
      },
      error: () => {
        this.cargando = false;
        this.error = 'No se pudo guardar el perfil.';
      }
    });
  }

  cerrarSesion(): void {
    this.auth.logout();
    this.router.navigate(['/login']);
  }
}
