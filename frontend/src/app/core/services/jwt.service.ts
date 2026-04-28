import { Injectable } from '@angular/core';

export interface JwtPayload {
  sub?: string;
  email?: string;
  role?: string;
  'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'?: string;
  'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'?: string;
  nbf?: number;
  exp?: number;
  iat?: number;
  iss?: string;
  aud?: string;
}

@Injectable({ providedIn: 'root' })
export class JwtService {

  decode(token: string): JwtPayload | null {
    try {
      const parts = token.split('.');
      if (parts.length !== 3) return null;

      const payload = parts[1];
      const padded = payload + '='.repeat((4 - (payload.length % 4)) % 4);
      const decoded = atob(padded.replace(/-/g, '+').replace(/_/g, '/'));
      return JSON.parse(decoded) as JwtPayload;
    } catch {
      return null;
    }
  }

  isExpired(token: string): boolean {
    const payload = this.decode(token);
    if (!payload?.exp) return true;
    return Date.now() >= payload.exp * 1000;
  }

  getRoleFromToken(token: string): string | null {
    const payload = this.decode(token);
    if (!payload) return null;

    // ASP.NET Core uses the long claim name for roles
    return (
      payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] ??
      payload.role ??
      null
    );
  }
}
