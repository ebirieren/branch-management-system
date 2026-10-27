import type { Product } from '../types/product'
import { Icon } from '../../../components/ui/Icon'
import { ProductCard } from '../components/ProductCard'


type ProductSectionProps = {
    products: Product[]
    isLoading: boolean
    error: string | null
    search: string
    onRefresh: () => Promise<void>
    onAddProduct: () => void
}

export function ProductSection({products, isLoading, error, search, onRefresh, onAddProduct}: ProductSectionProps){
    return (
        <section className="branch-section" id="products">
            <div className='section-heading'>
                <div>
                    <h2>Products</h2>
                </div>
                <div className='section-actions'>
                    <button className='primary-button' type="button" onClick={onAddProduct}>
                        <Icon name='plus' size={13} /> Add product
                    </button>
                    <button className='icon-button' type="button" onClick={() => void onRefresh()} disabled={isLoading} aria-label="Refresh products">
                        <Icon name="refresh" size={18} /> Refresh
                    </button>
                </div>

            </div>

            {error && (
                <div className="notice error-notice" role="alert">
                    <div><strong>We could not reach the API.</strong><p>{error}</p></div>
                    <button type="button" onClick={() => void onRefresh()}>Try again</button>
                </div>
            )}

            {isLoading ? (
                <div className="branch-grid" aria-label='Loading products'>
                    {[1,2,3].map((item) => <div className='branch-skeleton' key={item} />)}
                </div>
            ): products.length > 0 ? (
                <div className='branch-grid'>
                    {products.map((product,index) => (
                        <ProductCard key={product.id} product={product} accentIndex={index} />
                    ))}
                </div>
            ): (
                <div className='empty-state'>
                    <span>
                        <Icon name='box' size= {28} />
                    </span>

                    <h3>
                        {search ? "No matching products" : "No products yet"}
                    </h3>

                    {!search && (
                        <button className='primary-button' type="button" onClick={onAddProduct}>
                            <Icon name="plus" size={18} />
                            Add product 
                        </button>
                    )}
                </div>
            )}
        </section>
    )
}