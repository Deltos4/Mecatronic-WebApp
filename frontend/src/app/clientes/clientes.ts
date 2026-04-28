import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ApiService } from '../core/api.service';

interface Cliente {
  id_cliente: number;
  nombre_cliente: string;
  apellido_cliente: string;
  telefono_cliente: string;
  correo_usuario?: string;
  direccion_cliente: string;
  numero_documento_cliente: string;
  tipo_documento_cliente: 'DNI' | 'RUC' | 'CE' | 'PAS';
  estado_cliente: boolean;
}

@Component({
  selector: 'app-clientes',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './clientes.html',
  styleUrl: './clientes.css'
})
export class ClientesComponent implements OnInit {

  clientes: Cliente[] = [];
  cargando = false;
  error: string | null = null;

  form: Partial<Cliente> = this.defaultForm();
  editingId: number | null = null;

  constructor(
    private api: ApiService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.cargar();
  }

  cargar(): void {
    this.cargando = true;
    this.error = null;

    this.api.getClientes().subscribe({
      next: (data) => {
        this.clientes = data ?? [];
        this.cargando = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.error = 'Error al cargar clientes.';
        this.cargando = false;
        this.cdr.detectChanges();
      }
    });
  }

  guardar(): void {
    if (!this.form.nombre_cliente?.trim() || !this.form.apellido_cliente?.trim()) {
      alert('Completa nombre y apellido.');
      return;
    }

    if (!this.form.tipo_documento_cliente) {
      alert('Selecciona el tipo de documento.');
      return;
    }

    if (!this.form.numero_documento_cliente?.trim()) {
      alert('Ingresa el número de documento.');
      return;
    }

    if (this.form.tipo_documento_cliente === 'DNI' && !/^\d{8}$/.test(this.form.numero_documento_cliente)) {
      alert('El DNI debe tener exactamente 8 dígitos.');
      return;
    }

    if (this.form.tipo_documento_cliente === 'RUC' && !/^\d{11}$/.test(this.form.numero_documento_cliente)) {
      alert('El RUC debe tener exactamente 11 dígitos.');
      return;
    }

    if (
      this.form.telefono_cliente &&
      !/^9\d{8}$/.test(this.form.telefono_cliente)
    ) {
      alert('El celular debe tener 9 dígitos y empezar con 9.');
      return;
    }

    if (this.editingId === null) {
      this.api.createCliente(this.form).subscribe({
        next: () => {
          this.cargar();
          this.resetForm();
        },
        error: () => alert('Error al crear cliente.')
      });
    } else {
      this.api.updateCliente(this.editingId, this.form).subscribe({
        next: () => {
          this.cargar();
          this.resetForm();
        },
        error: () => alert('Error al actualizar cliente.')
      });
    }
  }

  editar(c: Cliente): void {
    this.form = {
      id_cliente: c.id_cliente,
      nombre_cliente: c.nombre_cliente,
      apellido_cliente: c.apellido_cliente,
      telefono_cliente: c.telefono_cliente,
      direccion_cliente: c.direccion_cliente,
      numero_documento_cliente: c.numero_documento_cliente,
      tipo_documento_cliente: c.tipo_documento_cliente,
      estado_cliente: c.estado_cliente,
      correo_usuario: c.correo_usuario
    };

    this.editingId = c.id_cliente;
  }

  eliminar(id: number): void {
    if (!confirm('¿Eliminar este cliente?')) return;

    this.api.deleteCliente(id).subscribe({
      next: () => this.cargar(),
      error: () => alert('Error al eliminar cliente.')
    });
  }

  cancelar(): void {
    this.resetForm();
  }

  onTipoDocChange(): void {
    this.form.numero_documento_cliente = '';
  }

  private defaultForm(): Partial<Cliente> {
    return {
      nombre_cliente: '',
      apellido_cliente: '',
      telefono_cliente: '',
      direccion_cliente: '',
      numero_documento_cliente: '',
      tipo_documento_cliente: 'DNI',
      estado_cliente: true
    };
  }

  private resetForm(): void {
    this.form = this.defaultForm();
    this.editingId = null;
  }
}