import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IProduct } from './models/products';
import { IPagination } from './models/pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'E-Shop';
  products: IProduct[] | undefined;
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get<IPagination>('http://localhost:5051/api/products?pageSize=50').subscribe((response: IPagination) => {
      this.products = response.data;
    }, error => console.log(error))
  }
}
