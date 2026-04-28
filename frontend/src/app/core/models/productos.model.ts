export interface ProductoDto {
  IdProducto: number;
  IdMarcaProducto: number;
  IdCategoriaProducto: number;
  IdProveedor?: number;
  NombreProducto: string;
  DescripcionProducto?: string;
  PrecioProducto: number;
  ImagenUrl?: string;
  EstadoProducto: boolean;
}

export interface ProductoCreateDto {
  IdMarcaProducto: number;
  IdCategoriaProducto: number;
  IdProveedor?: number;
  NombreProducto: string;
  DescripcionProducto?: string;
  PrecioProducto: number;
  ImagenUrl?: string;
}

export interface ProductoUpdateDto {
  IdMarcaProducto: number;
  IdCategoriaProducto: number;
  IdProveedor?: number;
  NombreProducto: string;
  DescripcionProducto?: string;
  PrecioProducto: number;
  ImagenUrl?: string;
  EstadoProducto: boolean;
}

export interface ProductoResponseDto {
  IdProducto: number;
  NombreProducto: string;
  NombreMarcaProducto: string;
  NombreCategoriaProducto: string;
  NombreProveedor?: string;
  DescripcionProducto?: string;
  PrecioProducto: number;
  ImagenUrl?: string;
  EstadoProducto: boolean;
}

export interface MarcaProductoDto {
  IdMarcaProducto: number;
  NombreMarcaProducto: string;
  DescripcionMarcaProducto?: string;
  EstadoMarcaProducto: boolean;
}

export interface CategoriaProductoDto {
  IdCategoriaProducto: number;
  NombreCategoriaProducto: string;
  DescripcionCategoriaProducto?: string;
}
