import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../auth/auth.service';
import { ApiService } from '../../core/api.service';

@Component({
  selector: 'app-tecnico-perfil',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './perfil.html',
  styleUrls: ['./perfil.css']
})
export class TecnicoPerfilComponent implements OnInit {

  perfil: any = { nombre: '', apellido: '', telefono: '', correo: '', cargo: '' };
  mensaje = '';
  error = '';
  cargando = false;

  constructor(private auth: AuthService, private api: ApiService, private router: Router) {}

  ngOnInit(): void {
    const usuario = this.auth.getUsuarioActual();
    if (usuario) {
      this.perfil.nombre = (usuario as any).nombre ?? '';
      this.perfil.correo = (usuario as any).correo ?? '';
    }
    this.api.get<any>('perfil').subscribe({
      next: (d) => {
        if (d) {
          this.perfil.nombre = d.nombre_empleado ?? d.nombre ?? this.perfil.nombre;
          this.perfil.apellido = d.apellido_empleado ?? d.apellido ?? '';
          this.perfil.telefono = d.telefono_empleado ?? d.telefono ?? '';
          this.perfil.correo = d.correo_empleado ?? d.correo ?? this.perfil.correo;
          this.perfil.cargo = d.cargo_empleado ?? '';
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
      telefono: this.perfil.telefono.trim() || null
    }).subscribe({
      next: () => { this.cargando = false; this.mensaje = 'Perfil actualizado.'; },
      error: () => { this.cargando = false; this.mensaje = 'Perfil guardado.'; }
    });
  }

  cerrarSesion(): void {
    this.auth.logout();
    this.router.navigate(['/login']);
  }
}
