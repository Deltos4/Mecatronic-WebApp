import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from '../auth.service';

export const staffGuard: CanActivateFn = () => {
  const auth = inject(AuthService);
  const router = inject(Router);

  if (!auth.isLoggedIn()) {
    router.navigate(['/login']);
    return false;
  }

  const role = auth.getRole();

  if (
    role === 'Administrador' ||
    role === 'Empleado' ||
    role === 'Tecnico'
  ) {
    return true;
  }

  router.navigate(['/login']);
  return false;
};