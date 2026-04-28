import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';

interface CarritoItem {
  id: number;
  idProducto?: number;
  nombre: string;
  precio: number;
  cantidad: number;
  tipo: 'PRODUCTO';
  stock?: number;
}

@Component({
  selector: 'app-carrito',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './carrito.html',
  styleUrls: ['./carrito.css']
})
export class CarritoComponent implements OnInit {

  items: CarritoItem[] = [];

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.cargarCarrito();
  }

  private cargarCarrito(): void {
    const data = localStorage.getItem('carrito');
    if (!data) { this.items = []; return; }
    try {
      const parsed = JSON.parse(data);
      this.items = Array.isArray(parsed) ? parsed : [];
    } catch {
      this.items = [];
    }
  }

  private guardarCarrito(): void {
    localStorage.setItem('carrito', JSON.stringify(this.items));
  }

  get total(): number {
    return this.items.reduce((sum, it) => sum + it.precio * it.cantidad, 0);
  }

  // IGV desglosado
  get subtotalSinIGV(): number { return this.total / 1.18; }
  get igv(): number { return this.total - this.subtotalSinIGV; }

  cambiarCantidad(item: CarritoItem, nuevaCantidad: number): void {
    if (nuevaCantidad <= 0) { this.eliminarItem(item); return; }
    // Validar stock si existe
    if (item.stock && nuevaCantidad > item.stock) {
      alert(`Solo hay ${item.stock} unidades disponibles.`);
      return;
    }
    item.cantidad = nuevaCantidad;
    this.guardarCarrito();
  }

  eliminarItem(item: CarritoItem): void {
    this.items = this.items.filter(i => (i.idProducto ?? i.id) !== (item.idProducto ?? item.id));
    this.guardarCarrito();
  }

  vaciarCarrito(): void {
    if (!confirm('¿Vaciar todo el carrito?')) return;
    this.items = [];
    this.guardarCarrito();
  }

  irAPago(): void {
    if (this.items.length === 0) { alert('El carrito está vacío.'); return; }

    const checkout = {
      items: this.items.map(i => ({
        ...i,
        idProducto: i.idProducto ?? i.id
      })),
      total: this.total
    };

    localStorage.setItem('checkout', JSON.stringify(checkout));
    this.router.navigateByUrl('/cliente/pago');
  }

  confirmarPedido(): void {
    this.irAPago();
  }
}
