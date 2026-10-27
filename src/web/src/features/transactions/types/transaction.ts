export type ProductTransaction = {
    productId: number
    productName: string
    supplierName: string
    productPrice: number
    taxRate: number
}

export type Transaction = {
    id: unknown
    branchId: number
    product: ProductTransaction
    quantity: number
    transactionDate: string
    totalExpense: number
    invoiceId: unknown
    isInvoiced: boolean
}