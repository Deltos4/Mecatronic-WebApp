import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class LoginComponent {
  CorreoUsuario = '';
  Contrasena = '';
  cargando = false;
  errorMessage: string | null = null;
  verPass = false;

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  onSubmit(): void {
    this.errorMessage = null;

    const correo = this.CorreoUsuario.trim().toLowerCase();
    const contrasena = this.Contrasena.trim();

    if (!correo || !contrasena) {
      this.errorMessage = 'Ingresa tu correo y contraseña.';
      return;
    }

    if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(correo)) {
      this.errorMessage = 'El correo está mal ingresado.';
      return;
    }

    this.cargando = true;

    this.authService.login(correo, contrasena).subscribe({
      next: (response) => {
        console.log('Respuesta del servidor:', response);

        setTimeout(() => {
          this.cargando = false;

          const rol = response.Usuario.Rol as string;

          if (rol === 'Cliente') {
            this.router.navigateByUrl('/catalogo');
            return;
          }
          if (rol === 'Tecnico') {
            this.router.navigateByUrl('/tecnico/panel');
            return;
          }
          if (rol === 'Administrador' || rol === 'Empleado') {
            this.router.navigateByUrl('/admin');
            return;
          }
          this.router.navigateByUrl('/login');
        }, 0);
      },
      error: (err) => {
        console.error('Error en login:', err);

        setTimeout(() => {
          this.cargando = false;

          const code = err?.error?.code as string | undefined;
          const message = (err?.error?.message ?? '').toString();
          const messageLower = message.toLowerCase();

          if (err.status === 404 || code === 'user_not_found' || messageLower.includes('usuario no encontrado') || messageLower.includes('correo')) {
            this.errorMessage = 'El correo no está registrado.';
          } else if (err.status === 401 || code === 'invalid_password' || messageLower.includes('contraseña')) {
            this.errorMessage = 'La contraseña ingresada es incorrecta.';
          } else if (err.status === 400 && message) {
            this.errorMessage = message;
          } else if (err.status === 403 && err.error?.sin_verificar) {
            this.errorMessage = '⚠️ Debes verificar tu correo...';
          } else {
            this.errorMessage = 'Error al conectar con el servidor. Intenta más tarde.';
          }
        }, 0);
      }
    });
  }
}
