import { Component, DoCheck, OnInit, signal } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ChatbotComponent } from './cliente_web/chatbot/chatbot';
import { AuthService } from './auth/auth.service';
import { RolUsuario } from './core/models/auth.model';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    RouterLink,
    RouterLinkActive,
    ChatbotComponent,
  ],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class AppComponent implements DoCheck, OnInit {
  protected readonly title = signal('mecatronic-web');
  cartCount = 0;
  role: RolUsuario | null = null;
  nombreUsuario: string | null = null;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.cargarSesion();
    this.actualizarCarrito();
  }

  ngDoCheck(): void {
    this.actualizarCarrito();
    this.cargarSesion();
  }

  private actualizarCarrito() {
    const data = localStorage.getItem('carrito');

    if (!data) {
      this.cartCount = 0;
      return;
    }

    try {
      const items: { cantidad: number }[] = JSON.parse(data);
      this.cartCount = items.reduce((sum, it) => sum + (it.cantidad || 0), 0);
    } catch {
      this.cartCount = 0;
    }
  }

  private cargarSesion() {
    const usuario = this.authService.getUsuarioActual();
    this.role = usuario?.Rol ?? null;
    this.nombreUsuario = usuario?.NombreUsuario ?? null;
  }

  logout() {
    this.authService.logout();
    this.role = null;
    this.nombreUsuario = null;
  }
}
