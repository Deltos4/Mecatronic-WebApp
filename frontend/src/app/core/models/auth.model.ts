export type RolUsuario = 'Administrador' | 'Empleado' | 'Tecnico' | 'Cliente';

export interface LoginRequest {
  CorreoUsuario: string;
  Contrasena: string;
}

export interface LoginResponse {
  IdUsuario: number;
  IdCliente?: number | null;
  Token: string;
  NombreUsuario: string;
  CorreoUsuario: string;
  Rol: string;
}

export interface RegisterRequest {
  IdRol: number;
  NombreUsuario: string;
  CorreoUsuario: string;
  Contrasena: string;
}

export interface UsuarioSesion {
  IdUsuario?: number;
  IdCliente?: number | null;
  NombreUsuario: string;
  Rol: RolUsuario;
  // Optional fields populated from profile data
  id?: number;
  nombre?: string;
  apellido?: string;
  telefono?: string;
  correoUsuario?: string;
}
