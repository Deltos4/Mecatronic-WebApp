export const API_BASE_URL = 'http://localhost:5107/api';

export const API_ENDPOINTS = {
  // Auth
  AUTH_LOGIN: 'auth/login',
  AUTH_REGISTER: 'auth/register',

  // Clientes
  CLIENTES: 'clientes',
  CLIENTE_BY_ID: (id: number) => `clientes/${id}`,

  // Productos
  PRODUCTOS: 'productos',
  PRODUCTO_BY_ID: (id: number) => `productos/${id}`,
  MARCAS_PRODUCTO: 'productos/marcas',
  CATEGORIAS_PRODUCTO: 'productos/categorias',

  // Órdenes de servicio
  ORDENES_SERVICIO: 'ordenservicio',
  ORDEN_BY_ID: (id: number) => `ordenservicio/${id}`,

  // Roles y usuarios
  ROLES: 'roles',
  USUARIOS: 'usuarios',
} as const;
