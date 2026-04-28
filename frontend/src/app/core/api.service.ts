import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

/**
 * Servicio central de comunicación con el backend.
 * Todos los componentes deben usar este servicio en lugar de
 * llamar a HttpClient directamente.
 *
 * TODO: cuando el backend esté desplegado, cambia solo BASE_URL.
 */
@Injectable({ providedIn: 'root' })
export class ApiService {

  // ─── CAMBIAR AQUÍ cuando el back esté listo ───────────────────────────────
  private readonly BASE_URL = 'http://localhost:5107/api';
  // ─────────────────────────────────────────────────────────────────────────

  constructor(private http: HttpClient) {}

  // ── Helpers internos ──────────────────────────────────────────────────────
  private headers(): HttpHeaders {
    const token = localStorage.getItem('authToken') ?? '';
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
  }

  get<T>(path: string): Observable<T> {
    return this.http.get<T>(`${this.BASE_URL}/${path}`, { headers: this.headers() });
  }

  post<T>(path: string, body: unknown): Observable<T> {
    return this.http.post<T>(`${this.BASE_URL}/${path}`, body, { headers: this.headers() });
  }

  put<T>(path: string, body: unknown): Observable<T> {
    return this.http.put<T>(`${this.BASE_URL}/${path}`, body, { headers: this.headers() });
  }

  delete<T>(path: string): Observable<T> {
    return this.http.delete<T>(`${this.BASE_URL}/${path}`, { headers: this.headers() });
  }

  // ── ENDPOINTS ─────────────────────────────────────────────────────────────

  // AUTH
  login(correo: string, contrasena: string) {
    return this.http.post<{ token: string; usuario: any }>(
      `${this.BASE_URL}/auth/login`, { correo, contrasena }
    );
  }

  // CLIENTES
  getClientes()              { return this.get<any[]>('clientes'); }
  createCliente(body: any)   { return this.post<any>('clientes', body); }
  updateCliente(id: number, body: any) { return this.put<any>(`clientes/${id}`, body); }
  deleteCliente(id: number)  { return this.delete<void>(`clientes/${id}`); }

  // VEHÍCULOS
  getVehiculos()             { return this.get<any[]>('vehiculos'); }
  createVehiculo(body: any)  { return this.post<any>('vehiculos', body); }
  updateVehiculo(id: number, body: any) { return this.put<any>(`vehiculos/${id}`, body); }
  deleteVehiculo(id: number) { return this.delete<void>(`vehiculos/${id}`); }

  // SERVICIOS
  getServicios()             { return this.get<any[]>('servicios'); }
  createServicio(body: any)  { return this.post<any>('servicios', body); }
  updateServicio(id: number, body: any) { return this.put<any>(`servicios/${id}`, body); }
  deleteServicio(id: number) { return this.delete<void>(`servicios/${id}`); }

  // TÉCNICOS
  getTecnicos()              { return this.get<any[]>('tecnicos'); }
  createTecnico(body: any)   { return this.post<any>('tecnicos', body); }
  updateTecnico(id: number, body: any) { return this.put<any>(`tecnicos/${id}`, body); }
  deleteTecnico(id: number)  { return this.delete<void>(`tecnicos/${id}`); }

  // ÓRDENES DE SERVICIO
  getOrdenes()               { return this.get<any[]>('ordenes-servicio'); }
  createOrden(body: any)     { return this.post<any>('ordenes-servicio', body); }
  updateOrden(id: number, body: any) { return this.put<any>(`ordenes-servicio/${id}`, body); }
  deleteOrden(id: number)    { return this.delete<void>(`ordenes-servicio/${id}`); }

  // PRODUCTOS (inventario / catálogo)
  getProductos()             { return this.get<any[]>('productos'); }
  createProducto(body: any)  { return this.post<any>('productos', body); }
  updateProducto(id: number, body: any) { return this.put<any>(`productos/${id}`, body); }
  deleteProducto(id: number) { return this.delete<void>(`productos/${id}`); }

  // PROVEEDORES
  getProveedores()           { return this.get<any[]>('proveedores'); }
  createProveedor(body: any) { return this.post<any>('proveedores', body); }
  updateProveedor(id: number, body: any) { return this.put<any>(`proveedores/${id}`, body); }
  deleteProveedor(id: number){ return this.delete<void>(`proveedores/${id}`); }

  // RESERVAS (citas)
  getReservas()              { return this.get<any[]>('reservas'); }
  createReserva(body: any)   { return this.post<any>('reservas', body); }
  updateReserva(id: number, body: any) { return this.put<any>(`reservas/${id}`, body); }

  // PEDIDOS
  getPedidos()               { return this.get<any[]>('pedidos'); }
  createPedido(body: any)    { return this.post<any>('pedidos', body); }

  // PAGOS
  getPagosMethods()          { return this.get<any[]>('metodos-pago'); }
  createPago(body: any)      { return this.post<any>('pagos', body); }

  // VENTAS (pedidos con comprobante)
  getVentas()                { return this.get<any[]>('ventas'); }
  createVenta(body: any)     { return this.post<any>('ventas', body); }

  // INVENTARIO — ENTRADAS / SALIDAS / KARDEX
  getEntradas()              { return this.get<any[]>('inventario/entradas'); }
  createEntrada(body: any)   { return this.post<any>('inventario/entradas', body); }
  getSalidas()               { return this.get<any[]>('inventario/salidas'); }
  createSalida(body: any)    { return this.post<any>('inventario/salidas', body); }
  getKardex(productoId: number) { return this.get<any[]>(`inventario/kardex/${productoId}`); }

  // REPORTES
  getReporteVentas(params?: string) { return this.get<any>(`reportes/ventas${params ? '?' + params : ''}`); }
  getReporteInventario()     { return this.get<any>('reportes/inventario'); }

  // COMPROBANTES (BOLETA / FACTURA)
  getComprobantes()          { return this.get<any[]>('comprobantes'); }
  createComprobante(body: any) { return this.post<any>('comprobantes', body); }

  // TIPOS DE COMPROBANTE
  getTiposComprobante()      { return this.get<any[]>('tipos-comprobante'); }

  // TIPOS DE DOCUMENTO
  getTiposDocumento()        { return this.get<any[]>('tipos-documento'); }

  // MARCAS / MODELOS / TIPOS VEHICULO
  getMarcasVehiculo()        { return this.get<any[]>('marcas-vehiculo'); }
  getModelosVehiculo()       { return this.get<any[]>('modelos-vehiculo'); }
  getTiposVehiculo()         { return this.get<any[]>('tipos-vehiculo'); }

  // ESPECIALIDADES
  getEspecialidades()        { return this.get<any[]>('especialidades'); }

  // MIS PEDIDOS (cliente autenticado)
  getMisPedidos()            { return this.get<any[]>('mis-pedidos'); }

  // MIS RESERVAS (cliente autenticado)
  getMisReservas()           { return this.get<any[]>('mis-reservas'); }
  cancelarReserva(id: number){ return this.put<any>(`reservas/${id}/cancelar`, {}); }

  // MARCAS / CATEGORÍAS PRODUCTO
  getMarcasProducto()        { return this.get<any[]>('marcas-producto'); }
  getCategoriasProducto()    { return this.get<any[]>('categorias-producto'); }
}
