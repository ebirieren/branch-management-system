export type BranchProduct = {
  id: number
  productName: string
  productPriceWithTax: number | null
  productCount: number
  initialProductValue: number | null
  lastShipment: string | null
}

export type Branch = {
  id: number
  branchName: string
  products: BranchProduct[]
  stockTransactions: unknown[]
  invoiceId: string
  updatedAt: string | null
  createdAt: string
}
