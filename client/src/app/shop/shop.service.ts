import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../shared/models/product';
import { Pagination } from '../shared/models/pagination';
import { Brand } from '../shared/models/brand';
import { Type } from '../shared/models/type';
import { ServiceResponse } from '../shared/models/serviceresponse';
import { ShopParams } from '../shared/models/shopParams';


@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = "https://localhost:7234/api/"

  constructor(private http: HttpClient) { }

  getProducts(shopParams: ShopParams) {
    let params = new HttpParams();

    if (shopParams.brandId > 0) params = params.append('brandId', shopParams.brandId);
    if (shopParams.typeId > 0) params = params.append('typeId', shopParams.typeId);
    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber);
    params = params.append('pageSize', shopParams.pageSize);
    if (shopParams.search) params = params.append('search', shopParams.search);

    return this.http.get<Pagination<Product[]>>(this.baseUrl + 'product', {params})
  }

  getProduct(id: number) {
    return this.http.get<ServiceResponse<Product>>(this.baseUrl + "product/" + id);
  }

  getBrands() {
    return this.http.get<ServiceResponse<Brand[]>>(this.baseUrl + 'product/brands');
  }

  getTypes() {
    return this.http.get<ServiceResponse<Type[]>>(this.baseUrl + 'product/types');
  }
}
