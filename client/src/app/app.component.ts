import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Product } from './product';
import { Pagination } from './pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Jono\s Site';
  products: Product[] = [];

  constructor(private http: HttpClient) {
  
  }
  ngOnInit(): void {
    this.http.get<Pagination<Product[]>>('https://localhost:7234/api/product?pageSize=50').subscribe({
      next: (response: any) => this.products = response.data, // what to do next
      error: error => console.log(error), // what to do if there is an error
      complete: () => {
        console.log('Request completed.');
        console.log('Nice work.');
      }
    })
  }
}