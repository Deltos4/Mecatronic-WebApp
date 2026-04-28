import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../api.service';
import { API_ENDPOINTS } from './api.config';
import {
  OrdenServicioDto,
  OrdenServicioCreateDto,
  OrdenServicioUpdateDto,
  OrdenServicioResponseDto,
} from '../models/ordenes.model';

@Injectable({ providedIn: 'root' })
export class OrdenesApiService {
  constructor(private api: ApiService) {}

  getAll(): Observable<OrdenServicioResponseDto[]> {
    return this.api.get<OrdenServicioResponseDto[]>(API_ENDPOINTS.ORDENES_SERVICIO);
  }

  getById(id: number): Observable<OrdenServicioDto> {
    return this.api.get<OrdenServicioDto>(API_ENDPOINTS.ORDEN_BY_ID(id));
  }

  create(body: OrdenServicioCreateDto): Observable<OrdenServicioDto> {
    return this.api.post<OrdenServicioDto>(API_ENDPOINTS.ORDENES_SERVICIO, body);
  }

  update(id: number, body: OrdenServicioUpdateDto): Observable<OrdenServicioDto> {
    return this.api.put<OrdenServicioDto>(API_ENDPOINTS.ORDEN_BY_ID(id), body);
  }

  delete(id: number): Observable<void> {
    return this.api.delete<void>(API_ENDPOINTS.ORDEN_BY_ID(id));
  }
}
