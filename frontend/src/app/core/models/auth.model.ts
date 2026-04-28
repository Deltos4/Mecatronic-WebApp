export type RolUsuario = 'Administrador' | 'Empleado' | 'Tecnico' | 'Cliente';

export interface LoginRequest {
  CorreoUsuario: string;
  Contrasena: string;
}

export interface LoginResponse {
  Token: string;
  NombreUsuario: string;
  Rol: string;
}

export interface RegisterRequest {
  IdRol: number;
  NombreUsuario: string;
  CorreoUsuario: string;
  Contrasena: string;
}

export interface UsuarioSesion {
  NombreUsuario: string;
  Rol: RolUsuario;
  // Optional fields populated from profile data
  id?: number;
  nombre?: string;
  apellido?: string;
  telefono?: string;
  correoUsuario?: string;
}
