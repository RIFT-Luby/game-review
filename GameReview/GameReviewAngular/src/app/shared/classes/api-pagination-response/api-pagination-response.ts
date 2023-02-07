export class ApiPaginationResponse<T> {
  skip!: number;
  take!: number;
  totalPages!: number;
  info!: T[];
}
