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

    if (!this.CorreoUsuario.trim() || !this.Contrasena.trim()) {
      this.errorMessage = 'Ingresa tu correo y contraseña.';
      return;
    }

    this.cargando = true;

    this.authService.login(this.CorreoUsuario.trim(), this.Contrasena.trim()).subscribe({
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

          if (err.status === 401 || err.status === 500) {
            this.errorMessage = 'Correo o contraseña incorrectos.';
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
