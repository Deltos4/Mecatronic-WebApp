import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../api.service';
import { API_ENDPOINTS } from './api.config';
import {
  ClienteDto,
  ClienteCreateDto,
  ClienteUpdateDto,
  ClienteResponseDto,
} from '../models/clientes.model';

@Injectable({ providedIn: 'root' })
export class ClientesApiService {
  constructor(private api: ApiService) {}

  getAll(): Observable<ClienteResponseDto[]> {
    return this.api.get<ClienteResponseDto[]>(API_ENDPOINTS.CLIENTES);
  }

  getById(id: number): Observable<ClienteDto> {
    return this.api.get<ClienteDto>(API_ENDPOINTS.CLIENTE_BY_ID(id));
  }

  create(body: ClienteCreateDto): Observable<ClienteDto> {
    return this.api.post<ClienteDto>(API_ENDPOINTS.CLIENTES, body);
  }

  update(id: number, body: ClienteUpdateDto): Observable<ClienteDto> {
    return this.api.put<ClienteDto>(API_ENDPOINTS.CLIENTE_BY_ID(id), body);
  }

  delete(id: number): Observable<void> {
    return this.api.delete<void>(API_ENDPOINTS.CLIENTE_BY_ID(id));
  }
}
