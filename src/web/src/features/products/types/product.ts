export type Product = {
    id: number
    productName: string
    productPrice: number | null
    previousPrice: number | null
    productCount: number | null
    supplierName: string
    taxRate : number
    productPriceWithTax: number
    initialTotalCost: number
    createdAt: string 
    updatedAt : string | null
}