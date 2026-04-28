import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { ApiService } from '../core/api.service';

export interface Producto {
  id: number;
  nombre: string;
  precio: number;
  stock: number;
}

@Injectable({
  providedIn: 'root'
})
export class ProductosService {

  constructor(private api: ApiService) {}

  getProductos(): Observable<Producto[]> {
    return this.api.getProductos().pipe(
      map((data: any[]) => (data ?? []).map(p => ({
        id: p.id_producto ?? p.id,
        nombre: p.nombre_producto ?? p.nombre,
        precio: p.precio_producto ?? p.precio ?? 0,
        stock: p.stock_producto ?? p.stock ?? 0
      })))
    );
  }

  actualizarStock(id: number, nuevoStock: number): Observable<any> {
    return this.api.updateProducto(id, { stock_producto: nuevoStock });
  }
}
