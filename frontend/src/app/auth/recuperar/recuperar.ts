import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { ApiService } from '../../core/api.service';

@Component({
  selector: 'app-recuperar',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './recuperar.html',
  styleUrl: './recuperar.css',
})
export class RecuperarComponent {
  correo = '';
  paso: 1 | 2 = 1;
  cargando = false;
  errorMessage = '';
  okMessage = '';

  constructor(private api: ApiService) {}

  buscarCuenta() {
    this.errorMessage = '';
    this.okMessage = '';
    const correo = this.correo.trim().toLowerCase();
    if (!correo) return this.setError('Ingresa tu correo.');

    this.cargando = true;
    this.api.post<any>('auth/recuperar', { correo }).subscribe({
      next: () => {
        this.cargando = false;
        this.paso = 2;
        this.okMessage = '✅ Te enviamos un correo con las instrucciones para restablecer tu contraseña. Revisa tu bandeja de entrada.';
      },
      error: (err) => {
        this.cargando = false;
        if (err.status === 404)
          this.setError('No encontramos una cuenta con ese correo.');
        else
          this.setError('Error al procesar la solicitud. Intenta de nuevo.');
      }
    });
  }

  private setError(msg: string) { this.errorMessage = msg; }
}
