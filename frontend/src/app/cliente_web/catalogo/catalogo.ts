import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { ApiService } from '../../core/api.service';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-catalogo',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './catalogo.html',
  styleUrl: './catalogo.css'
})
export class CatalogoComponent implements OnInit {

  productos: any[] = [];
  servicios: any[] = [];
  cargando = false;

  tabActiva: 'servicios' | 'repuestos' = 'servicios';
  filtroTexto = '';
  filtroCategoria = '';
  precioMin: number | null = null;
  precioMax: number | null = null;

  // Modal de login
  mostrarModalLogin = false;
  mensajeModal = '';

  constructor(
    private api: ApiService,
    private router: Router,
    private auth: AuthService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void { this.cargar(); }

  cargar(): void {
    this.cargando = true;
    this.api.getProductos().subscribe({
      next: (d) => { this.productos = d; this.cargando = false; this.cdr.detectChanges(); },
      error: () => { this.cargando = false; this.cdr.detectChanges(); }
    });
    this.api.getServicios().subscribe({
      next: (d) => { this.servicios = d; this.cdr.detectChanges(); },
      error: () => {}
    });
  }

  get categorias(): string[] {
    const lista = this.tabActiva === 'servicios' ? this.servicios : this.productos;
    const set = new Set(lista.map((x: any) => x.categoria ?? '').filter(Boolean));
    return Array.from(set);
  }

  get serviciosFiltrados(): any[] {
    return this.servicios.filter(s => {
      const texto = this.filtroTexto.trim().toLowerCase();
      const coincide = !texto ||
        (s.nombre_servicio ?? '').toLowerCase().includes(texto) ||
        (s.descripcion_servicio ?? '').toLowerCase().includes(texto);
      const precio = s.precio_servicio ?? 0;
      const minOk = this.precioMin === null || precio >= this.precioMin;
      const maxOk = this.precioMax === null || precio <= this.precioMax;
      return coincide && minOk && maxOk;
    }).map(s => ({
      ...s,
      nombre: s.nombre_servicio,
      descripcion: s.descripcion_servicio,
      precio: s.precio_servicio,
      duracion: s.duracion_servicio,
      imagen: s.imagen_url,
      stock: 1,
      id: s.id_servicio
    }));
  }

  get repuestosFiltrados(): any[] {
    return this.productos.filter(p => {
      const texto = this.filtroTexto.trim().toLowerCase();
      const coincide = !texto ||
        (p.nombre_producto ?? '').toLowerCase().includes(texto) ||
        (p.descripcion_producto ?? '').toLowerCase().includes(texto);
      const precio = p.precio_producto ?? 0;
      const minOk = this.precioMin === null || precio >= this.precioMin;
      const maxOk = this.precioMax === null || precio <= this.precioMax;
      return coincide && minOk && maxOk;
    }).map(p => ({
      ...p,
      nombre: p.nombre_producto,
      descripcion: p.descripcion_producto,
      precio: p.precio_producto,
      imagen: p.imagen_url,
      stock: p.stock_producto,
      id: p.id_producto
    }));
  }

  limpiarFiltros(): void {
    this.filtroTexto = '';
    this.filtroCategoria = '';
    this.precioMin = null;
    this.precioMax = null;
  }

  // Verificar si está logueado
  private estaLogueado(): boolean {
    return this.auth.isLoggedIn();
  }

  irACitas(servicio: any): void {
    if (!this.estaLogueado()) {
      this.mensajeModal = '¿Deseas reservar este servicio?';
      this.mostrarModalLogin = true;
      localStorage.setItem('cita_draft', JSON.stringify({
        servicioId: servicio.id,
        servicioNombre: servicio.nombre,
        precio: servicio.precio,
        duracion: servicio.duracion,
        categoria: ''
      }));
      return;
    }
    localStorage.setItem('cita_draft', JSON.stringify({
      servicioId: servicio.id,
      servicioNombre: servicio.nombre,
      precio: servicio.precio,
      duracion: servicio.duracion,
      categoria: ''
    }));
    this.router.navigate(['/cliente/citas/nueva', servicio.id]);
  }

  agregarRepuestoAlCarrito(producto: any): void {
    // Se puede agregar al carrito SIN estar logueado
    // El login se pide recién al momento de pagar
    const carrito = JSON.parse(localStorage.getItem('carrito') ?? '[]');
    const existe = carrito.find((c: any) => (c.idProducto ?? c.id) === producto.id);
    if (existe) {
      if (producto.stock && existe.cantidad >= producto.stock) {
        alert(`Solo hay ${producto.stock} unidades disponibles.`);
        return;
      }
      existe.cantidad++;
    } else {
      carrito.push({
        id: producto.id,
        idProducto: producto.id,
        nombre: producto.nombre,
        precio: producto.precio,
        cantidad: 1,
        tipo: 'PRODUCTO',
        stock: producto.stock,
        imagen: producto.imagen
      });
    }
    localStorage.setItem('carrito', JSON.stringify(carrito));
    this.mostrarMensajeCarrito(producto.nombre);
  }

  // Toast visual en vez de alert feo
  mensajeCarrito = '';
  mostrarMensajeCarrito(nombre: string): void {
    this.mensajeCarrito = `✅ "${nombre}" agregado al carrito`;
    setTimeout(() => this.mensajeCarrito = '', 3000);
  }

  irALogin(): void {
    this.mostrarModalLogin = false;
    this.router.navigate(['/login']);
  }

  irARegistro(): void {
    this.mostrarModalLogin = false;
    this.router.navigate(['/registro']);
  }

  cerrarModal(): void {
    this.mostrarModalLogin = false;
  }
}
