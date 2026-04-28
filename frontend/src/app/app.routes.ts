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
import { CitasComponent } from './cliente_web/citas/citas';
import { MisServiciosComponent } from './cliente_web/mis-servicios/mis-servicios';
import { MisPedidosComponent } from './cliente_web/mis-pedidos/mis-pedidos';
import { PerfilComponent } from './cliente_web/perfil/perfil';
import { RegistroComponent } from './cliente_web/registro/registro';

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
  { path: '', redirectTo: 'catalogo', pathMatch: 'full' }, // home = catálogo

  // ===== CLIENTE (requiere login) =====
  {
    path: 'cliente',
    canActivate: [clienteGuard],
    children: [
      { path: 'carrito', component: CarritoComponent },
      { path: 'citas', component: CitasComponent },
      { path: 'citas/nueva/:idServicio', component: CitasComponent },
      { path: 'mis-servicios', component: MisServiciosComponent },
      { path: 'mis-pedidos', component: MisPedidosComponent },
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
  { path: '**', redirectTo: 'catalogo' },
];