export interface ClienteDto {
  IdCliente: number;
  IdUsuario?: number;
  IdTipoDocumento: number;
  NombreTipoDocumento: string;
  NumeroDocumentoCliente: string;
  NombreCliente: string;
  ApellidoCliente: string;
  TelefonoCliente?: string;
  DireccionCliente?: string;
  FechaRegistro: string;
  EstadoCliente: boolean;
}

export interface ClienteCreateDto {
  IdUsuario?: number;
  IdTipoDocumento: number;
  NumeroDocumentoCliente: string;
  NombreCliente: string;
  ApellidoCliente: string;
  TelefonoCliente?: string;
  DireccionCliente?: string;
}

export interface ClienteUpdateDto {
  IdTipoDocumento: number;
  NumeroDocumentoCliente: string;
  NombreCliente: string;
  ApellidoCliente: string;
  TelefonoCliente?: string;
  DireccionCliente?: string;
}

export interface ClienteResponseDto {
  IdCliente: number;
  NombreCompleto?: string;
  NombreTipoDocumento?: string;
  NumeroDocumentoCliente?: string;
  TelefonoCliente?: string;
  DireccionCliente?: string;
  EstadoCliente: boolean;
  Vehiculos: DetalleClienteVehiculoResponseDto[];
}

export interface DetalleClienteVehiculoResponseDto {
  IdVehiculo: number;
  NombreModelo: string;
  NombreMarca: string;
  NombreTipo: string;
  PlacaVehiculo: string;
  AnioVehiculo?: number;
  ColorVehiculo?: string;
  UrlFotoVehiculo?: string;
  EstadoVehiculo: boolean;
}
