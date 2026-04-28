import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../core/api.service';

interface Tecnico {
  id_tecnico: number;
  dni_empleado: string;
  nombre_empleado: string;
  apellido_empleado: string;
  telefono_empleado: string;
  correo_empleado: string;
  cargo_empleado: string;
  estado_empleado: boolean;
}

@Component({
  selector: 'app-tecnicos',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './tecnicos.html',
  styleUrls: ['./tecnicos.css']
})
export class TecnicosComponent implements OnInit {

  tecnicos: Tecnico[] = [];
  cargando = false;
  error: string | null = null;

  filtroTexto = '';
  filtroEstado = '';

  mostrarFormulario = false;
  modoEdicion = false;
  editingId: number | null = null;

  tecnicoForm: Partial<Tecnico> = this.defaultForm();

  constructor(private api: ApiService, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void { this.cargar(); }

  cargar(): void {
    this.cargando = true;
    this.api.getTecnicos().subscribe({
      next: (d) => { this.tecnicos = d; this.cargando = false; this.cdr.detectChanges(); },
      error: () => { this.error = 'Error al cargar técnicos.'; this.cargando = false; this.cdr.detectChanges(); }
    });
  }

  get tecnicosFiltrados(): Tecnico[] {
    const t = this.filtroTexto.trim().toLowerCase();
    return this.tecnicos.filter(tc => {
      const coincideTexto = !t ||
        tc.nombre_empleado.toLowerCase().includes(t) ||
        tc.apellido_empleado.toLowerCase().includes(t) ||
        tc.dni_empleado.includes(t) ||
        tc.cargo_empleado.toLowerCase().includes(t);
      const coincideEstado = !this.filtroEstado ||
        (this.filtroEstado === 'Activo' ? tc.estado_empleado : !tc.estado_empleado);
      return coincideTexto && coincideEstado;
    });
  }

  nuevoTecnico(): void {
    this.modoEdicion = false;
    this.tecnicoForm = this.defaultForm();
    this.mostrarFormulario = true;
  }

  editar(id: number): void {
    const t = this.tecnicos.find(tc => tc.id_tecnico === id);
    if (!t) return;
    this.modoEdicion = true;
    this.editingId = id;
    this.tecnicoForm = { ...t };
    this.mostrarFormulario = true;
  }

  guardar(): void {
    if (!this.tecnicoForm.nombre_empleado?.trim()) { alert('Ingresa el nombre.'); return; }
    if (!this.tecnicoForm.apellido_empleado?.trim()) { alert('Ingresa el apellido.'); return; }

    if (!this.modoEdicion) {
      this.api.createTecnico(this.tecnicoForm).subscribe({
        next: () => { this.cargar(); this.cancelar(); },
        error: () => alert('Error al crear técnico.')
      });
    } else {
      this.api.updateTecnico(this.editingId!, this.tecnicoForm).subscribe({
        next: () => { this.cargar(); this.cancelar(); },
        error: () => alert('Error al actualizar técnico.')
      });
    }
  }

  eliminar(id: number): void {
    if (!confirm('¿Eliminar este técnico?')) return;
    this.api.deleteTecnico(id).subscribe({
      next: () => this.cargar(),
      error: () => alert('Error al eliminar técnico.')
    });
  }

  cancelar(): void {
    this.mostrarFormulario = false;
    this.modoEdicion = false;
    this.editingId = null;
    this.tecnicoForm = this.defaultForm();
  }

  limpiarFiltros(): void { this.filtroTexto = ''; this.filtroEstado = ''; }

  private defaultForm(): Partial<Tecnico> {
    return { dni_empleado: '', nombre_empleado: '', apellido_empleado: '',
             telefono_empleado: '', correo_empleado: '', cargo_empleado: '', estado_empleado: true };
  }
}
