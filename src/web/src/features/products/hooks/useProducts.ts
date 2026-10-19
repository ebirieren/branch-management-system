import { useEffect, useState } from "react"
import { createProduct as createProductRequest, getProducts } from "../api/products"
import type { Product } from "../../../types/product"

export function useProducts(){
    const [products, setProducts] = useState<Product[]>([])
    const [error, setError] = useState<string | null>(null)
    const [isLoading, setIsLoading] = useState(true)

    async function refreshProducts(): Promise<void> {
        try {
            setIsLoading(true)
            setError(null)

            const loadedProducts = await getProducts()

            setProducts(loadedProducts)
        } catch (requestError) {
            setError(requestError instanceof Error ? requestError.message : "Something went wrong.")
        } finally {
            setIsLoading(false)
        }
    }

    async function addProduct(productName: string, productPrice: number, productCount: number, supplierName: string, taxRate: number): Promise<boolean> {
        try {
            setError(null)

            const newProduct = await createProductRequest(productName, productPrice, productCount, supplierName, taxRate)

            setProducts((currentProducts) => [newProduct, ...currentProducts])

            return true
        } catch (requestError){
            setError(requestError instanceof Error ? requestError.message : "Could not create the product.")

            return false
        }
    }

    useEffect(() => {
        let ignoreResult = false

        void getProducts()
        .then((data) => {
            if (!ignoreResult) setProducts(data)
        })
        .catch((requestError: unknown) => {
            if (!ignoreResult) {
                setError(requestError instanceof Error ? requestError.message : "Something went wrong.")
            }
        })
        .finally(() => {
            if (!ignoreResult) setIsLoading(false)
        })

        return () => {ignoreResult = true}
    }, [])

    return { products, isLoading, error, refreshProducts, addProduct}
}