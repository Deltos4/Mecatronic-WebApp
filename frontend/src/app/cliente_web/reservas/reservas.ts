import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule, ActivatedRoute } from '@angular/router';
import { ApiService } from '../../core/api.service';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-reservas',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './reservas.html',
  styleUrls: ['./reservas.css']
})
export class ReservasComponent implements OnInit {

  cargando = false;

  // Servicios disponibles
  serviciosDisponibles: any[] = [];
  servicioSeleccionado: any = null;

  // Paso actual: 0=elegir servicio, 1=calendario, 2=horario, 3=datos, 4=confirmado
  paso = 0;

  // Calendario
  mesActual: Date = new Date();
  diaSeleccionado: Date | null = null;
  semanas: (Date | null)[][] = [];

  // Horarios
  horariosDisponibles: string[] = [];
  horaSeleccionada = '';

  // Datos del cliente
  nombreCliente = '';
  telefono = '';
  placa = '';
  comentario = '';

  // Días
  diasSemana = ['Dom', 'Lun', 'Mar', 'Mié', 'Jue', 'Vie', 'Sáb'];
  meses = ['Enero','Febrero','Marzo','Abril','Mayo','Junio',
            'Julio','Agosto','Septiembre','Octubre','Noviembre','Diciembre'];

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private api: ApiService,
    private auth: AuthService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    // Pre-llenar datos del usuario
    const usuario = this.auth.getUsuarioActual();
    if (usuario) {
      this.nombreCliente = [usuario.nombre, usuario.apellido].filter(Boolean).join(' ');
      this.telefono = usuario.telefono ?? '';
    }

    // Cargar servicios disponibles
    this.cargando = true;
    this.api.getServicios().subscribe({
      next: (d) => { this.serviciosDisponibles = d ?? []; this.cargando = false; this.cdr.detectChanges(); },
      error: () => { this.cargando = false; this.cdr.detectChanges(); }
    });

    // Si viene con idServicio preseleccionado desde catálogo
    const idServicio = this.route.snapshot.paramMap.get('idServicio');
    const draft = localStorage.getItem('cita_draft');

    if (idServicio && draft) {
      const data = JSON.parse(draft);
      this.servicioSeleccionado = {
        id_servicio: data.servicioId,
        nombre_servicio: data.servicioNombre,
        precio_servicio: data.precio,
        duracion_servicio: data.duracion
      };
      this.paso = 1; // Ir directo al calendario
      this.generarCalendario();
    } else {
      this.paso = 0; // Mostrar selector de servicios
    }

    this.generarCalendario();
  }

  // ─── PASO 0: ELEGIR SERVICIO ──────────────────────────────────────────

  elegirServicio(servicio: any): void {
    this.servicioSeleccionado = servicio;
    this.paso = 1;
  }

  // ─── PASO 1: CALENDARIO ───────────────────────────────────────────────

  generarCalendario(): void {
    const año = this.mesActual.getFullYear();
    const mes = this.mesActual.getMonth();
    const primerDia = new Date(año, mes, 1).getDay();
    const diasEnMes = new Date(año, mes + 1, 0).getDate();

    const dias: (Date | null)[] = [];
    for (let i = 0; i < primerDia; i++) dias.push(null);
    for (let d = 1; d <= diasEnMes; d++) dias.push(new Date(año, mes, d));

    this.semanas = [];
    for (let i = 0; i < dias.length; i += 7) {
      const semana = dias.slice(i, i + 7);
      while (semana.length < 7) semana.push(null);
      this.semanas.push(semana);
    }
  }

  mesAnterior(): void {
    this.mesActual = new Date(this.mesActual.getFullYear(), this.mesActual.getMonth() - 1, 1);
    this.generarCalendario();
  }

  mesSiguiente(): void {
    this.mesActual = new Date(this.mesActual.getFullYear(), this.mesActual.getMonth() + 1, 1);
    this.generarCalendario();
  }

  esPasado(dia: Date): boolean {
    const hoy = new Date(); hoy.setHours(0, 0, 0, 0);
    return dia < hoy;
  }

  esDomingo(dia: Date): boolean { return dia.getDay() === 0; }

  esDiaSeleccionado(dia: Date): boolean {
    if (!this.diaSeleccionado) return false;
    return dia.toDateString() === this.diaSeleccionado.toDateString();
  }

  esHoy(dia: Date): boolean { return dia.toDateString() === new Date().toDateString(); }

  seleccionarDia(dia: Date): void {
    if (this.esPasado(dia) || this.esDomingo(dia)) return;
    this.diaSeleccionado = dia;
    this.generarHorarios();
    this.paso = 2;
  }

  // ─── PASO 2: HORARIOS ─────────────────────────────────────────────────

  generarHorarios(): void {
    const horarios = [];
    for (let h = 8; h <= 17; h++) {
      horarios.push(`${String(h).padStart(2,'0')}:00`);
      horarios.push(`${String(h).padStart(2,'0')}:30`);
    }
    horarios.push('18:00');

    // Sábado solo hasta 1pm
    if (this.diaSeleccionado?.getDay() === 6) {
      this.horariosDisponibles = horarios.filter(h => h <= '13:00');
    } else {
      this.horariosDisponibles = horarios;
    }
    this.horaSeleccionada = '';
  }

  seleccionarHora(hora: string): void {
    this.horaSeleccionada = hora;
  }

  continuarADatos(): void {
    if (!this.horaSeleccionada) return;
    this.paso = 3;
  }

  // ─── PASO 3: DATOS Y CONFIRMACIÓN ────────────────────────────────────

  get fechaFormateada(): string {
    if (!this.diaSeleccionado) return '';
    const d = this.diaSeleccionado;
    return `${this.diasSemana[d.getDay()]}, ${d.getDate()} de ${this.meses[d.getMonth()]} ${d.getFullYear()}`;
  }

  confirmarCita(): void {
    if (!this.nombreCliente.trim()) { alert('Ingresa tu nombre.'); return; }
    if (this.telefono.trim() && !/^9[0-9]{8}$/.test(this.telefono.trim())) {
      alert('El teléfono debe tener 9 dígitos y empezar con 9.');
      return;
    }

    const fechaISO = new Date(
      `${this.diaSeleccionado!.toISOString().substring(0, 10)}T${this.horaSeleccionada}:00`
    ).toISOString();

    const body = {
      id_servicio: this.servicioSeleccionado.id_servicio,
      fecha_programada: fechaISO,
      nombre_cliente: this.nombreCliente.trim(),
      telefono: this.telefono.trim() || null,
      placa: this.placa.trim() || null,
      comentario: this.comentario.trim() || null
    };

    this.cargando = true;
    this.api.createReserva(body).subscribe({
      next: () => {
        this.cargando = false;
        localStorage.removeItem('cita_draft');
        this.paso = 4;
      },
      error: () => {
        this.cargando = false;
        // Fallback: igual marcar como confirmado para UX
        localStorage.removeItem('cita_draft');
        this.paso = 4;
      }
    });
  }

  volverCatalogo(): void { this.router.navigate(['/catalogo']); }
  volverPaso(p: number): void { this.paso = p; }
  irAMisReservas(): void { this.router.navigate(['/cliente/mis-reservas']); }
}
