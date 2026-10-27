import type { Product } from "../types/product"

const PRODUCTS_URL = '/api/products'

export async function getProducts(): Promise<Product[]> {
    const response = await fetch(PRODUCTS_URL)

    if (!response.ok){
        throw new Error(`Request failed with status ${response.status}. Is the backend running?`)
    }
    
    return response.json() as Promise<Product[]>
}

export async function createProduct(productName: string, productPrice: number, productCount: number, supplierName: string, taxRate: number): Promise<Product>{
    const response = await fetch(`${PRODUCTS_URL}/create`, {
        method: "POST",
        headers: { 'Content-Type': 'application/json'},
        body: JSON.stringify({productName, productPrice, productCount, supplierName, taxRate}),
    })

    if (!response.ok){
        throw new Error(`Could not create the product (status ${response.status}).`)
    }

    return response.json() as Promise<Product>
}