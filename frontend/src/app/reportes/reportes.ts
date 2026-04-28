import { Component, OnInit, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../core/api.service';
import Chart from 'chart.js/auto';
import jsPDF from 'jspdf';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-reportes',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './reportes.html',
  styleUrl: './reportes.css'
})
export class ReportesComponent implements OnInit, AfterViewInit {

  ventas: any[] = [];
  totalIngresos = 0;
  ventasDia = 0;
  ventasMes = 0;
  totalCitas = 0;

  fechaInicio = '';
  fechaFin = '';

  @ViewChild('chartVentasDia') chartVentasDiaRef!: ElementRef<HTMLCanvasElement>;
  @ViewChild('chartMetodoPago') chartMetodoPagoRef!: ElementRef<HTMLCanvasElement>;
  chartVentasDia?: Chart;
  chartMetodoPago?: Chart;

  constructor(private api: ApiService) {}

  ngOnInit(): void { this.cargar(); }

  ngAfterViewInit(): void { this.dibujarGraficos(); }

  cargar(): void {
    this.api.getReservas().subscribe({
      next: (d) => this.totalCitas = d.length,
      error: () => {}
    });
    this.api.getReporteVentas().subscribe({
      next: (d) => {
        this.ventas = Array.isArray(d) ? d : (d.ventas ?? []);
        this.calcularResumen();
        this.dibujarGraficos();
      },
      error: () => {}
    });
  }

  calcularResumen(): void {
    const hoy = new Date().toISOString().substring(0, 10);
    const mes = new Date().getMonth();
    this.totalIngresos = this.ventas.reduce((s, v) => s + (v.total ?? 0), 0);
    this.ventasDia = this.ventas
      .filter(v => new Date(v.fecha_pedido ?? v.fecha).toISOString().substring(0, 10) === hoy)
      .reduce((s, v) => s + (v.total ?? 0), 0);
    this.ventasMes = this.ventas
      .filter(v => new Date(v.fecha_pedido ?? v.fecha).getMonth() === mes)
      .reduce((s, v) => s + (v.total ?? 0), 0);
  }

  ventasFiltradas(): any[] {
    if (!this.fechaInicio || !this.fechaFin) return this.ventas;
    const f1 = new Date(this.fechaInicio), f2 = new Date(this.fechaFin);
    return this.ventas.filter(v => {
      const f = new Date(v.fecha_pedido ?? v.fecha);
      return f >= f1 && f <= f2;
    });
  }

  aplicarFiltros(): void { this.dibujarGraficos(); }

  private dibujarGraficos(): void {
    const ventas = this.ventasFiltradas();
    const agrupado: Record<string, number> = {};
    const porMetodo: Record<string, number> = {};
    for (const v of ventas) {
      const clave = new Date(v.fecha_pedido ?? v.fecha).toISOString().substring(0, 10);
      agrupado[clave] = (agrupado[clave] || 0) + (v.total ?? 0);
      const m = v.metodo_pago ?? v.metodoPago ?? 'Otro';
      porMetodo[m] = (porMetodo[m] || 0) + (v.total ?? 0);
    }
    if (this.chartVentasDia) this.chartVentasDia.destroy();
    if (this.chartMetodoPago) this.chartMetodoPago.destroy();
    if (!this.chartVentasDiaRef || !this.chartMetodoPagoRef) return;

    const etiq = Object.keys(agrupado).sort();
    this.chartVentasDia = new Chart(this.chartVentasDiaRef.nativeElement, {
      type: 'line',
      data: { labels: etiq, datasets: [{ label: 'Ventas por día (S/)', data: etiq.map(k => agrupado[k]) }] },
      options: { responsive: true }
    });
    const etiqM = Object.keys(porMetodo);
    this.chartMetodoPago = new Chart(this.chartMetodoPagoRef.nativeElement, {
      type: 'bar',
      data: { labels: etiqM, datasets: [{ label: 'Por método de pago (S/)', data: etiqM.map(k => porMetodo[k]) }] },
      options: { responsive: true, scales: { y: { beginAtZero: true } } }
    });
  }

  descargarPDF(): void {
    const doc = new jsPDF();
    doc.setFontSize(14); doc.text('Reporte de ventas', 10, 10);
    let y = 20;
    for (const v of this.ventasFiltradas()) {
      doc.setFontSize(10);
      doc.text(`${v.id_pedido ?? v.id}  ${new Date(v.fecha_pedido ?? v.fecha).toLocaleDateString()}  S/ ${v.total}`, 10, y);
      y += 6; if (y > 280) { doc.addPage(); y = 20; }
    }
    doc.save('reporte-ventas.pdf');
  }

  descargarExcel(): void {
    const filas = this.ventasFiltradas().map(v => ({
      ID: v.id_pedido ?? v.id,
      Fecha: new Date(v.fecha_pedido ?? v.fecha).toLocaleDateString(),
      Total: v.total
    }));
    const hoja = XLSX.utils.json_to_sheet(filas);
    const libro = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(libro, hoja, 'Ventas');
    XLSX.writeFile(libro, 'reporte-ventas.xlsx');
  }
}
