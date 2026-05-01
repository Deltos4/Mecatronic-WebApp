import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs';
import { map } from 'rxjs/operators';
import { JwtService } from '../core/services/jwt.service';
import { RolUsuario, LoginResponse, UsuarioSesion } from '../core/models/auth.model';
import { API_BASE_URL, API_ENDPOINTS } from '../core/api/api.config';

export type { RolUsuario };

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly storageKey = 'usuarioActual';
  private readonly tokenKey = 'authToken';

  constructor(private http: HttpClient, private jwt: JwtService) {}

  login(correo: string, contrasena: string) {
    const body = { CorreoUsuario: correo, Contrasena: contrasena };

    return this.http.post<LoginResponse>(`${API_BASE_URL}/${API_ENDPOINTS.AUTH_LOGIN}`, body).pipe(
      map(res => {
        const { nombre, apellido } = this.splitNombre(res.NombreUsuario);
        return {
          Token: res.Token,
          Usuario: {
            IdUsuario: res.IdUsuario,
            IdCliente: res.IdCliente ?? null,
            NombreUsuario: res.NombreUsuario,
            correoUsuario: res.CorreoUsuario,
            nombre,
            apellido,
            // Backend always returns Rol; JWT fallback is a safety net in case it changes
            Rol: (res.Rol || this.jwt.getRoleFromToken(res.Token)) as RolUsuario
          } as UsuarioSesion
        };
      }),
      tap(response => {
        localStorage.setItem(this.tokenKey, response.Token);
        localStorage.setItem(this.storageKey, JSON.stringify(response.Usuario));
      })
    );
  }

  register(body: { IdRol: number; NombreUsuario: string; CorreoUsuario: string; Contrasena: string }) {
    return this.http.post(`${API_BASE_URL}/${API_ENDPOINTS.AUTH_REGISTER}`, body);
  }

  logout(): void {
    localStorage.removeItem(this.storageKey);
    localStorage.removeItem(this.tokenKey);
  }

  getUsuarioActual(): UsuarioSesion | null {
    const data = localStorage.getItem(this.storageKey);
    if (!data) return null;
    try {
      return JSON.parse(data) as UsuarioSesion;
    } catch {
      return null;
    }
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  getRole(): RolUsuario | null {
    return this.getUsuarioActual()?.Rol ?? null;
  }

  isLoggedIn(): boolean {
    const token = this.getToken();
    if (!token) return false;
    if (this.jwt.isExpired(token)) {
      this.logout();
      return false;
    }
    return this.getUsuarioActual() !== null;
  }

  isTokenExpired(): boolean {
    const token = this.getToken();
    return token ? this.jwt.isExpired(token) : true;
  }

  private splitNombre(nombreCompleto: string): { nombre: string; apellido: string } {
    const partes = (nombreCompleto ?? '').trim().split(/\s+/).filter(Boolean);
    if (partes.length === 0) return { nombre: '', apellido: '' };
    if (partes.length === 1) return { nombre: partes[0], apellido: '' };
    return { nombre: partes[0], apellido: partes.slice(1).join(' ') };
  }
}
