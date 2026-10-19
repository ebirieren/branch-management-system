import type { CSSProperties } from "react"
import type { Product } from "../../../types/product"

type ProductCardProps = {
    product: Product
    accentIndex: number
}

const accentColors = ['#2478f0', '#7659d6', '#e7782b', '#1b9c69']

export function ProductCard({ product, accentIndex }: ProductCardProps){
   const initials = product.productName
        .split(' ')
        .filter(Boolean)
        .slice(0,2)
        .map((word) => word[0])
        .join('')
        .toUpperCase()

        const stockStatus = getStockStatus(product.productCount)


    return (
        <article className="branch-card" style={{ '--branch-accent': accentColors[accentIndex % accentColors.length] } as CSSProperties}>
           
            <div className="branch-card-header">
                <span className="branch-initials">
                    {initials || "PR" }
                </span>

                <div>
                    <h3>{product.productName}</h3>
                    <span className="branch-status">
                        {stockStatus}
                    </span>
                </div>
            </div>

            <div className="branch-metrics">
                <div>
                    <span>Price with tax</span>
                    <strong>
                        {formatPrice(product.productPriceWithTax)}
                    </strong>
                </div>
                <div>
                    <span>Units in stock</span>
                    <strong>
                        {product.productCount === null ? 'Unknown' : product.productCount.toLocaleString()}
                    </strong>
                </div>
            </div>

            <footer className="branch-footer">
                <span>
                    Supplier: {product.supplierName || 'Unknown'}
                </span>

                <span className="branch-link">
                    Product #{product.id}
                </span>
            </footer>
        </article>
    )
}
function getStockStatus(productCount: number | null ): string {
 if(productCount === null){
    return "Stock unkown"
 }

 if(productCount === 0) {
    return "Out of stock"
 }

 return "In stock"
}

function formatPrice(price: number): string{
    return new Intl.NumberFormat('tr-TR', {
        style: 'currency',
        currency: 'TRY',
    }).format(price)
} 