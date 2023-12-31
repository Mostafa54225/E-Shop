import { Component, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/models/products';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct | undefined

  constructor(private shopService: ShopService, private activateRoute: ActivatedRoute) {}
  ngOnInit(): void {
    this.loadProduct()
  }

  
  loadProduct() {
    const idParam = this.activateRoute.snapshot.paramMap.get('id')
    if(!idParam) return
    const productId = +idParam
    if(isNaN(productId) || productId <= 0) {
      return
    } 
    this.shopService.getProduct(+productId).subscribe(product => {
      this.product = product
    }, error => console.log(error))
  }
}
