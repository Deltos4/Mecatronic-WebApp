import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../api.service';
import { API_ENDPOINTS } from './api.config';
import {
  ProductoDto,
  ProductoCreateDto,
  ProductoUpdateDto,
  ProductoResponseDto,
  MarcaProductoDto,
  CategoriaProductoDto,
} from '../models/productos.model';

@Injectable({ providedIn: 'root' })
export class ProductosApiService {
  constructor(private api: ApiService) {}

  getAll(): Observable<ProductoResponseDto[]> {
    return this.api.get<ProductoResponseDto[]>(API_ENDPOINTS.PRODUCTOS);
  }

  getById(id: number): Observable<ProductoDto> {
    return this.api.get<ProductoDto>(API_ENDPOINTS.PRODUCTO_BY_ID(id));
  }

  create(body: ProductoCreateDto): Observable<ProductoDto> {
    return this.api.post<ProductoDto>(API_ENDPOINTS.PRODUCTOS, body);
  }

  update(id: number, body: ProductoUpdateDto): Observable<ProductoDto> {
    return this.api.put<ProductoDto>(API_ENDPOINTS.PRODUCTO_BY_ID(id), body);
  }

  delete(id: number): Observable<void> {
    return this.api.delete<void>(API_ENDPOINTS.PRODUCTO_BY_ID(id));
  }

  getMarcas(): Observable<MarcaProductoDto[]> {
    return this.api.get<MarcaProductoDto[]>(API_ENDPOINTS.MARCAS_PRODUCTO);
  }

  getCategorias(): Observable<CategoriaProductoDto[]> {
    return this.api.get<CategoriaProductoDto[]>(API_ENDPOINTS.CATEGORIAS_PRODUCTO);
  }
}
