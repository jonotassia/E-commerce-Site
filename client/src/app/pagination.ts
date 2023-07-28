export interface Pagination<T> {
    success: boolean
    message: string
    pageIndex: number
    pageSize: number
    count: number
    data: T[]
  }
  