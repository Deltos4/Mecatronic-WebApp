import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-registro',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './registro.html',
  styleUrl: './registro.css',
})
export class RegistroComponent {
  nombre = '';
  apellido = '';
  correo = '';
  password = '';
  password2 = '';

  cargando = false;
  errorMessages: string[] = [];
  okMessage = '';
  verPass = false;
  verPass2 = false;

  constructor(
    private router: Router,
    private authService: AuthService
  ) {}

  get tieneMayuscula(): boolean { return /[A-Z]/.test(this.password); }
  get tieneNumero(): boolean { return /[0-9]/.test(this.password); }
  get tieneEspecial(): boolean { return /[!@#$%^&*()_+\-=\[\]{};:,.<>?]/.test(this.password); }

  async registrar() {
    this.errorMessages = [];
    this.okMessage = '';

    const nombre = this.nombre.trim();
    const apellido = this.apellido.trim();
    const correo = this.correo.trim().toLowerCase();
    const pass = this.password;
    const pass2 = this.password2;

    const errores: string[] = [];
    const soloLetras = /^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$/;

    if (!nombre || nombre.length < 2) {
      errores.push('Ingresa tu nombre (mínimo 2 caracteres).');
    } else if (!soloLetras.test(nombre)) {
      errores.push('El nombre solo debe contener letras.');
    }

    if (!apellido || apellido.length < 2) {
      errores.push('Ingresa tu apellido (mínimo 2 caracteres).');
    } else if (!soloLetras.test(apellido)) {
      errores.push('El apellido solo debe contener letras.');
    }

    if (!correo || !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(correo)) {
      errores.push('Correo electrónico no válido.');
    }

    if (!pass || pass.length < 8) {
      errores.push('La contraseña debe tener mínimo 8 caracteres.');
    }
    if (pass && !/[A-Z]/.test(pass)) {
      errores.push('La contraseña debe tener al menos una letra mayúscula.');
    }
    if (pass && !/[0-9]/.test(pass)) {
      errores.push('La contraseña debe tener al menos un número.');
    }
    if (pass && !/[!@#$%^&*()_+\-=\[\]{};:,.<>?]/.test(pass)) {
      errores.push('La contraseña debe tener al menos un carácter especial (!@#$%...).');
    }
    if (pass !== pass2) {
      errores.push('Las contraseñas no coinciden.');
    }

    if (errores.length > 0) {
      this.errorMessages = errores;
      return;
    }

    this.cargando = true;

    try {
      await firstValueFrom(this.authService.register({
        IdRol: 4,
        NombreUsuario: `${nombre} ${apellido}`.trim(),
        CorreoUsuario: correo,
        Contrasena: pass
      }));

      this.okMessage = '¡Cuenta creada exitosamente! Redirigiendo al login...';
      setTimeout(() => this.router.navigateByUrl('/login'), 1500);
    } catch (err: any) {
      const mensaje = (err?.error ?? '').toString().toLowerCase();
      if (err?.status === 409 || mensaje.includes('correo')) {
        this.errorMessages = ['Ese correo ya está registrado.'];
      } else {
        this.errorMessages = ['Error al crear la cuenta. Intenta de nuevo.'];
      }
    } finally {
      this.cargando = false;
    }
  }
}
