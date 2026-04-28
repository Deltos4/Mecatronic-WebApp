import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ApiService } from '../../core/api.service';

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
  telefono = '';
  password = '';
  password2 = '';

  cargando = false;
  errorMessage = '';
  okMessage = '';
  verPass = false;
  verPass2 = false;

  constructor(private router: Router, private api: ApiService) {}

  get tieneMayuscula(): boolean { return /[A-Z]/.test(this.password); }
  get tieneNumero(): boolean { return /[0-9]/.test(this.password); }
  get tieneEspecial(): boolean { return /[!@#$%^&*()_+\-=\[\]{};:,.<>?]/.test(this.password); }

  registrar() {
    this.errorMessage = '';
    this.okMessage = '';

    const nombre   = this.nombre.trim();
    const apellido = this.apellido.trim();
    const correo   = this.correo.trim().toLowerCase();
    const telefono = this.telefono.trim();
    const pass     = this.password;
    const pass2    = this.password2;

    if (!nombre || nombre.length < 2)
      return this.setError('Ingresa tu nombre (mínimo 2 caracteres).');
    if (!apellido || apellido.length < 2)
      return this.setError('Ingresa tu apellido (mínimo 2 caracteres).');
    if (!correo || !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(correo))
      return this.setError('Correo electrónico no válido.');
    if (telefono && !/^9[0-9]{8}$/.test(telefono))
      return this.setError('Celular inválido. Debe tener 9 dígitos y empezar con 9.');
    if (!pass || pass.length < 8)
      return this.setError('La contraseña debe tener mínimo 8 caracteres.');
    if (!/[A-Z]/.test(pass))
      return this.setError('La contraseña debe tener al menos una letra mayúscula.');
    if (!/[0-9]/.test(pass))
      return this.setError('La contraseña debe tener al menos un número.');
    if (!/[!@#$%^&*()_+\-=\[\]{};:,.<>?]/.test(pass))
      return this.setError('La contraseña debe tener al menos un carácter especial (!@#$%...).');
    if (pass !== pass2)
      return this.setError('Las contraseñas no coinciden.');

    this.cargando = true;

    this.api.post<any>('auth/register', {
      nombre,
      apellido,
      correo,
      telefono: telefono || null,
      contrasena: pass,
      id_rol: 4  // rol Cliente
    }).subscribe({
      next: () => {
        this.okMessage = '¡Cuenta creada exitosamente! Redirigiendo al login...';
        this.cargando = false;
        setTimeout(() => this.router.navigateByUrl('/login'), 1500);
      },
      error: (err) => {
        this.cargando = false;
        if (err.status === 409)
          this.setError('Ese correo ya está registrado.');
        else
          this.setError('Error al crear la cuenta. Intenta de nuevo.');
      }
    });
  }

  private setError(msg: string) { this.errorMessage = msg; }
}
