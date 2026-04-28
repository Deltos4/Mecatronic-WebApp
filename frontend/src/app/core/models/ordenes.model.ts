export interface OrdenServicioDto {
  IdOrdenServicio: number;
  IdCliente?: number;
  IdReserva?: number;
  IdPedido?: number;
  IdProforma?: number;
  IdVehiculo?: number;
  PlacaVehiculo?: string;
  IdTecnico?: number;
  FechaInicio: string;
  FechaFin?: string;
  Total: number;
  TipoItem: number;
  Estado?: string;
}

export interface OrdenServicioCreateDto {
  IdCliente?: number;
  IdReserva?: number;
  IdPedido?: number;
  IdProforma?: number;
  IdVehiculo?: number;
  IdTecnico?: number;
  FechaInicio: string;
  Total: number;
  TipoItem: number;
}

export interface OrdenServicioUpdateDto {
  Estado?: string;
  FechaFin?: string;
}

export interface OrdenServicioResponseDto {
  IdOrdenServicio: number;
  IdCliente?: number;
  NombreCliente?: string;
  IdReserva?: number;
  IdPedido?: number;
  IdProforma?: number;
  IdVehiculo?: number;
  IdTecnico?: number;
  FechaInicio: string;
  FechaFin?: string;
  Total: number;
  TipoItem: number;
  DetalleOrden: DetalleOrdenResponseDto[];
}

export interface DetalleOrdenDto {
  IdDetalleOP: number;
  IdProducto: number;
  NombreProducto: string;
  Descripcion?: string;
  Cantidad: number;
  PrecioUnitario: number;
  Subtotal: number;
}

export interface DetalleOrdenCreateDto {
  IdProducto: number;
  Cantidad: number;
  PrecioUnitario: number;
}

export interface DetalleOrdenResponseDto {
  IdDetalleOP: number;
  IdProducto: number;
  NombreProducto: string;
  Descripcion?: string;
  Cantidad: number;
  PrecioUnitario: number;
  Subtotal: number;
}
