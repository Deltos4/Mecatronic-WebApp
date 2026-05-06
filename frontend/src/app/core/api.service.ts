import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, map } from 'rxjs';

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
    const normalizedPath = this.normalizePath(path);
    return this.http
      .get<T>(`${this.BASE_URL}/${normalizedPath}`, { headers: this.headers() })
      .pipe(map((data) => this.toSnakeCase(data) as T));
  }

  post<T>(path: string, body: unknown): Observable<T> {
    const normalizedPath = this.normalizePath(path);
    const payload = this.toPascalCase(body);
    return this.http
      .post<T>(`${this.BASE_URL}/${normalizedPath}`, payload, { headers: this.headers() })
      .pipe(map((data) => this.toSnakeCase(data) as T));
  }

  put<T>(path: string, body: unknown): Observable<T> {
    const normalizedPath = this.normalizePath(path);
    const payload = this.toPascalCase(body);
    return this.http
      .put<T>(`${this.BASE_URL}/${normalizedPath}`, payload, { headers: this.headers() })
      .pipe(map((data) => this.toSnakeCase(data) as T));
  }

  delete<T>(path: string): Observable<T> {
    const normalizedPath = this.normalizePath(path);
    return this.http
      .delete<T>(`${this.BASE_URL}/${normalizedPath}`, { headers: this.headers() })
      .pipe(map((data) => this.toSnakeCase(data) as T));
  }

  // ── ENDPOINTS ─────────────────────────────────────────────────────────────

  // AUTH
  login(correo: string, contrasena: string) {
    return this.http.post<{ token: string; usuario: any }>(
      `${this.BASE_URL}/auth/login`, { CorreoUsuario: correo, Contrasena: contrasena }
    );
  }

  // CLIENTES
  getClientes()              { return this.get<any[]>('clientes'); }
  createCliente(body: any)   { return this.post<any>('clientes', body); }
  updateCliente(id: number, body: any) { return this.put<any>(`clientes/${id}`, body); }
  deleteCliente(id: number)  { return this.delete<void>(`clientes/${id}`); }

  // VEHÍCULOS
  getVehiculos()             { return this.get<any[]>('vehiculos'); }
  getVehiculosCliente(idCliente: number) { return this.get<any[]>(`vehiculos/cliente/${idCliente}`); }
  createVehiculo(body: any)  { return this.post<any>('vehiculos', body); }
  updateVehiculo(id: number, body: any) { return this.put<any>(`vehiculos/${id}`, body); }
  deleteVehiculo(id: number) { return this.delete<void>(`vehiculos/${id}`); }
  asignarVehiculoCliente(body: any) { return this.post<void>('vehiculos/asignar', body); }

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
  getOrdenes()               { return this.get<any[]>('ordenservicio'); }
  createOrden(body: any)     { return this.post<any>('ordenservicio', body); }
  updateOrden(id: number, body: any) { return this.put<any>(`ordenservicio/${id}`, body); }
  deleteOrden(id: number)    { return this.delete<void>(`ordenservicio/${id}`); }

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
  getPagosMethods()          { return this.get<any[]>('pagos/metodos'); }
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
  getProformas()             { return this.get<any[]>('proformas'); }
  getProformasCliente(idCliente: number) { return this.get<any[]>(`proformas/cliente/${idCliente}`); }
  createComprobante(body: any) { return this.post<any>('comprobantes/generar', body); }

  // TIPOS DE COMPROBANTE
  getTiposComprobante()      { return this.get<any[]>('maestros/tipos-comprobante'); }

  // TIPOS DE DOCUMENTO
  getTiposDocumento()        { return this.get<any[]>('maestros/tipos-documento'); }
  consultarDni(dni: string)  { return this.get<any>(`clientes/consulta-dni/${dni}`); }

  // MARCAS / MODELOS / TIPOS VEHICULO
  getMarcasVehiculo()        { return this.get<any[]>('vehiculos/marcas'); }
  getModelosVehiculo()       { return this.get<any[]>('vehiculos/modelos'); }
  getTiposVehiculo()         { return this.get<any[]>('vehiculos/tipos'); }

  // ESPECIALIDADES
  getEspecialidades()        { return this.get<any[]>('tecnicos/especialidades'); }

  // MIS PEDIDOS (cliente autenticado)
  getMisPedidos()            { return this.get<any[]>('mis-pedidos'); }

  // MIS RESERVAS (cliente autenticado)
  getMisReservas()           { return this.get<any[]>('mis-reservas'); }
  cancelarReserva(id: number){ return this.put<any>(`reservas/${id}/cancelar`, {}); }

  // MARCAS / CATEGORÍAS PRODUCTO
  getMarcasProducto()        { return this.get<any[]>('productos/marcas'); }
  getCategoriasProducto()    { return this.get<any[]>('productos/categorias'); }

  private normalizePath(path: string): string {
    const replacements: Array<[RegExp, string]> = [
      [/^ordenes-servicio\b/i, 'ordenservicio'],
      [/^metodos-pago\b/i, 'pagos/metodos'],
      [/^tipos-comprobante\b/i, 'maestros/tipos-comprobante'],
      [/^tipos-documento\b/i, 'maestros/tipos-documento'],
      [/^marcas-vehiculo\b/i, 'vehiculos/marcas'],
      [/^modelos-vehiculo\b/i, 'vehiculos/modelos'],
      [/^tipos-vehiculo\b/i, 'vehiculos/tipos'],
      [/^marcas-producto\b/i, 'productos/marcas'],
      [/^categorias-producto\b/i, 'productos/categorias'],
    ];

    for (const [pattern, replacement] of replacements) {
      if (pattern.test(path)) {
        return path.replace(pattern, replacement);
      }
    }

    return path;
  }

  private toSnakeCase(value: unknown): unknown {
    if (Array.isArray(value)) {
      return value.map((item) => this.toSnakeCase(item));
    }

    if (this.isPlainObject(value)) {
      const mapped: Record<string, unknown> = {};
      Object.entries(value).forEach(([key, val]) => {
        mapped[this.toSnakeCaseKey(key)] = this.toSnakeCase(val);
      });
      return mapped;
    }

    return value;
  }

  private toPascalCase(value: unknown): unknown {
    if (Array.isArray(value)) {
      return value.map((item) => this.toPascalCase(item));
    }

    if (this.isPlainObject(value)) {
      const mapped: Record<string, unknown> = {};
      Object.entries(value).forEach(([key, val]) => {
        mapped[this.toPascalCaseKey(key)] = this.toPascalCase(val);
      });
      return mapped;
    }

    return value;
  }

  private toSnakeCaseKey(key: string): string {
    return key
      .replace(/([a-z0-9])([A-Z])/g, '$1_$2')
      .replace(/([A-Z]+)([A-Z][a-z0-9]+)/g, '$1_$2')
      .toLowerCase();
  }

  private toPascalCaseKey(key: string): string {
    if (key.includes('_')) {
      return key
        .split('_')
        .filter(Boolean)
        .map((part) => part.charAt(0).toUpperCase() + part.slice(1))
        .join('');
    }

    return key.charAt(0).toUpperCase() + key.slice(1);
  }

  private isPlainObject(value: unknown): value is Record<string, unknown> {
    return typeof value === 'object' && value !== null && !Array.isArray(value);
  }
}
