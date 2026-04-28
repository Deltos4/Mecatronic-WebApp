import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ApiService } from '../../core/api.service';
import { AuthService } from '../../auth/auth.service';

interface CheckoutItem {
  idServicio?: number;
  idProducto?: number;
  id?: number;
  nombre: string;
  precio: number;
  cantidad: number;
  tipo: 'PRODUCTO' | 'SERVICIO';
}

type MetodoPago = 'YAPE' | 'PLIN' | 'TRANSFERENCIA' | 'TARJETA' | 'CONTADO';

@Component({
  standalone: true,
  selector: 'app-pago',
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './pago.html',
  styleUrls: ['./pago.css']
})
export class PagoComponent implements OnInit {
  items: CheckoutItem[] = [];
  total = 0;

  estaLogueado = false;

  // Datos cliente
  nombres = '';
  apellido = '';
  celular = '';
  correo = '';
  direccion = '';

  // Datos extra para servicios
  placa = '';
  comentarioServicio = '';

  // Comprobante SUNAT
  tipoComprobante: 'BOLETA' | 'FACTURA' = 'BOLETA';
  dni = '';
  ruc = '';
  razonSocial = '';
  direccionFiscal = '';

  // Pago
  metodo: MetodoPago = 'YAPE';
  referenciaPago = '';
  metodosPago: any[] = [];

  tieneProductos = false;
  tieneServicios = false;

  // Entrega / cita
  metodoEntrega: 'RECOJO_EN_TIENDA' | 'CITA' = 'RECOJO_EN_TIENDA';
  fechaCita = '';
  horaCita = '';

  cargando = false;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private api: ApiService,
    private auth: AuthService
  ) {}

  ngOnInit(): void {
    this.estaLogueado = this.auth.isLoggedIn();
    this.cargarCheckout();
    this.definirFlujo();
    this.cargarDatosUsuario();

    // Cargar métodos de pago desde BD
    this.api.getPagosMethods().subscribe({
      next: (d) => this.metodosPago = d ?? [],
      error: () => {}
    });
  }

  private cargarCheckout(): void {
    const raw = localStorage.getItem('checkout');
    if (!raw) { alert('No hay datos de pago. Vuelve al carrito.'); this.router.navigateByUrl('/carrito'); return; }
    try {
      const data = JSON.parse(raw);
      this.items = data.items ?? [];
      this.total = data.total ?? 0;
      if (this.items.length === 0) { alert('Tu carrito está vacío.'); this.router.navigateByUrl('/carrito'); }
    } catch {
      alert('Datos de pago inválidos.'); this.router.navigateByUrl('/carrito');
    }
  }

  private definirFlujo(): void {
    this.tieneProductos = this.items.some(i => i.tipo === 'PRODUCTO');
    this.tieneServicios = this.items.some(i => i.tipo === 'SERVICIO');
    this.metodoEntrega = this.tieneServicios ? 'CITA' : 'RECOJO_EN_TIENDA';
  }

  private cargarDatosUsuario(): void {
    const usuario = this.auth.getUsuarioActual();
    if (!usuario) return;
    this.nombres = usuario.nombre ?? '';
    this.apellido = (usuario as any).apellido ?? '';
    this.celular = (usuario as any).telefono ?? '';
    this.correo = (usuario as any).correo ?? '';
  }

  // IGV
  get subtotalSinIGV(): number { return this.total / 1.18; }
  get igv(): number { return this.total - this.subtotalSinIGV; }

  cambiarMetodo(m: MetodoPago): void {
    this.metodo = m;
    this.referenciaPago = '';
  }

  irALogin(): void { this.router.navigate(['/login'], { queryParams: { returnUrl: '/cliente/pago' } }); }
  irACrearCuenta(): void { this.router.navigate(['/registro'], { queryParams: { returnUrl: '/cliente/pago' } }); }

  validar(): string | null {
    if (!this.estaLogueado) return 'Debes iniciar sesión para continuar.';
    if (!this.nombres.trim()) return 'Ingresa tus nombres.';
    if (this.celular.trim() && !/^9\d{8}$/.test(this.celular.trim())) return 'Celular inválido (9 dígitos, empieza con 9).';
    if (!this.correo.trim() || !this.correo.includes('@')) return 'Correo inválido.';

    if (this.tipoComprobante === 'BOLETA') {
      if (!/^\d{8}$/.test(this.dni.trim())) return 'DNI inválido (8 dígitos).';
    } else {
      if (!/^(10|20)\d{9}$/.test(this.ruc.trim())) return 'RUC inválido (11 dígitos, empieza con 10 o 20).';
      if (!this.razonSocial.trim()) return 'Ingresa la razón social.';
      if (!this.direccionFiscal.trim()) return 'Ingresa la dirección fiscal.';
    }

    if ((this.metodo === 'YAPE' || this.metodo === 'PLIN' || this.metodo === 'TRANSFERENCIA') && this.referenciaPago.trim().length < 4) {
      return 'Ingresa tu número de operación/referencia.';
    }
    if (this.metodo === 'TARJETA' && this.referenciaPago.trim().length < 6) {
      return 'Ingresa un código de confirmación de tarjeta.';
    }

    if (this.metodoEntrega === 'CITA') {
      if (!this.fechaCita) return 'Selecciona la fecha de la cita.';
      if (!this.horaCita) return 'Selecciona la hora de la cita.';
      if (!this.placa.trim()) return 'Ingresa la placa del vehículo.';
    }

    return null;
  }

  pagarAhora(): void {
    if (!this.estaLogueado) {
      alert('Debes iniciar sesión.'); this.irALogin(); return;
    }

    const err = this.validar();
    if (err) { alert(err); return; }

    const body = {
      items: this.items.map(i => ({
        id_producto: i.idProducto ?? i.id ?? null,
        id_servicio: i.idServicio ?? null,
        nombre: i.nombre,
        cantidad: i.cantidad,
        precio_unitario: i.precio,
        subtotal: i.precio * i.cantidad,
        tipo: i.tipo
      })),
      total: this.total,
      subtotal: this.subtotalSinIGV,
      igv: this.igv,

      // Comprobante SUNAT
      tipo_comprobante: this.tipoComprobante,
      dni: this.tipoComprobante === 'BOLETA' ? this.dni.trim() : null,
      ruc: this.tipoComprobante === 'FACTURA' ? this.ruc.trim() : null,
      razon_social: this.tipoComprobante === 'FACTURA' ? this.razonSocial.trim() : null,
      direccion_fiscal: this.tipoComprobante === 'FACTURA' ? this.direccionFiscal.trim() : null,

      // Pago
      metodo_pago: this.metodo,
      referencia_pago: this.referenciaPago.trim(),

      // Cliente
      nombre_cliente: this.nombres.trim(),
      apellido_cliente: this.apellido.trim(),
      celular: this.celular.trim() || null,
      correo: this.correo.trim(),
      direccion: this.direccion.trim() || null,

      // Entrega
      metodo_entrega: this.metodoEntrega,
      fecha_cita: this.metodoEntrega === 'CITA' ? `${this.fechaCita}T${this.horaCita}` : null,
      placa: this.placa.trim() || null,
      comentario: this.comentarioServicio.trim() || null
    };

    this.cargando = true;

    this.api.createPedido(body).subscribe({
      next: (res) => {
        this.cargando = false;
        localStorage.removeItem('carrito');
        localStorage.removeItem('checkout');
        alert(this.metodoEntrega === 'CITA'
          ? 'Pago registrado ✅ Cita generada ✅'
          : 'Pago registrado ✅ Pedido generado ✅');
        this.router.navigateByUrl(this.metodoEntrega === 'CITA' ? '/cliente/mis-servicios' : '/cliente/mis-pedidos');
      },
      error: () => {
        this.cargando = false;
        alert('Error al procesar el pago. Intenta de nuevo.');
      }
    });
  }
}
