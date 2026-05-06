import { Routes } from '@angular/router';

// ===== INTERNO =====
import { ClientesComponent } from './clientes/clientes';
import { VehiculosComponent } from './vehiculos/vehiculos';
import { ServiciosComponent } from './servicios/servicios';
import { OrdenesComponent } from './ordenes/ordenes';
import { VentasComponent } from './ventas/ventas';
import { ReportesComponent } from './reportes/reportes';
import { TecnicosComponent } from './tecnicos/tecnicos';

// ===== TÉCNICO =====
import { PanelComponent } from './tecnico/panel/panel';

// ===== INVENTARIO =====
import { InventarioShellComponent } from './inventario/inventario-shell/inventario-shell';
import { RepuestosComponent } from './inventario/repuestos/repuestos';
import { EntradasComponent } from './inventario/entradas/entradas';
import { SalidasComponent } from './inventario/salidas/salidas';
import { ProveedoresComponent } from './inventario/proveedores/proveedores';
import { KardexComponent } from './inventario/kardex/kardex';

// ===== CLIENTE =====
import { CatalogoComponent } from './cliente_web/catalogo/catalogo';
import { CarritoComponent } from './cliente_web/carrito/carrito';
import { ReservasComponent } from './cliente_web/reservas/reservas';
import { MisReservasComponent } from './cliente_web/mis-reservas/mis-reservas';
import { MisPedidosComponent } from './cliente_web/mis-pedidos/mis-pedidos';
import { PerfilComponent } from './cliente_web/perfil/perfil';
import { RegistroComponent } from './cliente_web/registro/registro';
import { Landing } from './cliente_web/landing/landing';
import { VehiculosClienteComponent } from './cliente_web/vehiculos/vehiculos';
import { ProformasClienteComponent } from './cliente_web/proformas/proformas';

// ===== AUTH =====
import { LoginComponent } from './auth/login/login';
import { RecuperarComponent } from './auth/recuperar/recuperar';
import { staffGuard } from './auth/guards/staff.guard';
import { clienteGuard } from './auth/guards/cliente-guard';

export const routes: Routes = [
  // ===== PUBLICO =====
  { path: 'login', component: LoginComponent },
  { path: 'recuperar', component: RecuperarComponent },
  { path: 'registro', component: RegistroComponent },
  { path: 'catalogo', component: CatalogoComponent }, // público sin login
  { path: 'carrito', component: CarritoComponent },    // público sin login
  { path: '', component: Landing }, // home = landing

  // ===== CLIENTE (requiere login) =====
  {
      path: 'cliente',
      canActivate: [clienteGuard],
      children: [
        { path: 'carrito', component: CarritoComponent },
        { path: 'reservas', component: ReservasComponent },
        { path: 'reservas/nueva/:idServicio', component: ReservasComponent },
        { path: 'mis-pedidos', component: MisPedidosComponent },
        { path: 'mis-reservas', component: MisReservasComponent },
        { path: 'vehiculos', component: VehiculosClienteComponent },
        { path: 'proformas', component: ProformasClienteComponent },
        { path: 'perfil', component: PerfilComponent },
        {
          path: 'pago',
        loadComponent: () =>
          import('./cliente_web/pago/pago').then(m => m.PagoComponent),
      },
    ],
  },

  // ===== TÉCNICO =====
  {
    path: 'tecnico',
    canActivate: [staffGuard],
    children: [
      { path: '', redirectTo: 'panel', pathMatch: 'full' },
      { path: 'panel', component: PanelComponent },
    ],
  },

  // ===== ADMIN =====
  {
    path: 'admin',
    canActivate: [staffGuard],
    children: [
      { path: '', redirectTo: 'ventas', pathMatch: 'full' },

      { path: 'clientes', component: ClientesComponent },
      { path: 'vehiculos', component: VehiculosComponent },
      { path: 'servicios', component: ServiciosComponent },
      { path: 'ordenes', component: OrdenesComponent },
      { path: 'tecnicos', component: TecnicosComponent },

      {
        path: 'proforma',
        loadComponent: () =>
          import('./proforma/proforma').then(m => m.ProformaComponent),
      },
      {
        path: 'proforma/:id',
        loadComponent: () =>
          import('./proforma/proforma').then(m => m.ProformaComponent),
      },

      {
        path: 'inventario',
        component: InventarioShellComponent,
        children: [
          { path: '', redirectTo: 'repuestos', pathMatch: 'full' },
          { path: 'repuestos', component: RepuestosComponent },
          { path: 'entradas', component: EntradasComponent },
          { path: 'salidas', component: SalidasComponent },
          { path: 'proveedores', component: ProveedoresComponent },
          { path: 'kardex', component: KardexComponent },
        ],
      },

      { path: 'ventas', component: VentasComponent },
      { path: 'reportes', component: ReportesComponent },
    ],
  },

  // ===== DEFAULT =====
  { path: '**', redirectTo: '' },
];
