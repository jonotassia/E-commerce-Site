import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../shared/models/product';
import { Pagination } from '../shared/models/pagination';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = "https://localhost:7234/api/"

  constructor(private http: HttpClient) { }

  getProducts() {
    return this.http.get<Pagination<Product[]>>(this.baseUrl + 'product?pageSize=50')
  }
}
