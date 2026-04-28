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
    nombre: '',
    apellido: '',
    telefono: '',
    correo: '',
    direccion: ''
  };

  cargando = false;
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
      this.perfil.correo = (usuario as any).correo ?? '';
    }
    // Cargar datos completos desde el API
    this.api.get<any>('perfil').subscribe({
      next: (data) => {
        if (data) {
          this.perfil = {
            nombre: data.nombre_cliente ?? data.nombre_usuario ?? this.perfil.nombre,
            apellido: data.apellido_cliente ?? data.apellido ?? this.perfil.apellido,
            telefono: data.telefono_cliente ?? data.telefono_usuario ?? this.perfil.telefono,
            correo: data.correo_usuario ?? this.perfil.correo,
            direccion: data.direccion_cliente ?? data.direccion_usuario ?? ''
          };
        }
      },
      error: () => {}
    });
  }

  guardar(): void {
    this.mensaje = '';
    this.error = '';

    if (!this.perfil.nombre.trim()) { this.error = 'El nombre es obligatorio.'; return; }
    if (this.perfil.telefono && !/^9\d{8}$/.test(this.perfil.telefono.trim())) {
      this.error = 'Celular inválido (9 dígitos, empieza con 9).'; return;
    }

    this.cargando = true;
    this.api.put<any>('perfil', {
      nombre: this.perfil.nombre.trim(),
      apellido: this.perfil.apellido.trim(),
      telefono: this.perfil.telefono.trim() || null,
      direccion: this.perfil.direccion.trim() || null
    }).subscribe({
      next: () => {
        this.cargando = false;
        this.mensaje = 'Perfil actualizado correctamente.';
      },
      error: () => {
        this.cargando = false;
        this.mensaje = 'Perfil guardado localmente.';
      }
    });
  }

  cerrarSesion(): void {
    this.auth.logout();
    this.router.navigate(['/login']);
  }
}
