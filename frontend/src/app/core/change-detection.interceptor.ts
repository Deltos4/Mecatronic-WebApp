import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { ApplicationRef } from '@angular/core';
import { tap, finalize } from 'rxjs';

export const changeDetectionInterceptor: HttpInterceptorFn = (req, next) => {
  const appRef = inject(ApplicationRef);
  return next(req).pipe(
    finalize(() => {
      // Forzar detección de cambios solo cuando la petición termina
      Promise.resolve().then(() => appRef.tick());
    })
  );
};
