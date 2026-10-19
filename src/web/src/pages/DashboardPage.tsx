import { useMemo, useState } from 'react'
import { DashboardStats } from '../components/dashboard/DashboardStats'
import { Icon } from '../components/ui/Icon'
import { useBranches } from '../features/branches/hooks/useBranches'
import { useProducts } from '../features/products/hooks/useProducts'
import { DashboardLayout } from '../layouts/DashboardLayout'
import { CreateBranchDialog } from '../features/branches/components/CreateBranchDialog'
import { CreateProductDialog } from '../features/products/components/CreateProductDialog'
import { BranchSection } from '../features/branches/components/BranchSection'
import { ProductSection } from '../features/products/components/ProductSection'

export function DashboardPage(){
    const { branches, isLoading: isBranchesLoading, error: branchesError, refreshBranches, addBranch} = useBranches()
    const { products, isLoading: isProductsLoading, error: productsError, refreshProducts, addProduct} = useProducts()
    const [search, setSearch] = useState('')
    const [isBranchDialogOpen, setIsBranchDialogOpen] = useState(false)
    const [isProductDialogOpen, setIsProductDialogOpen] = useState(false)

    const filteredBranches = useMemo(() => {
        const term = search.trim().toLowerCase()
        if (!term) return branches
        return branches.filter((branch) => branch.branchName.toLowerCase().includes(term))
    }, [branches, search])

    const filteredProducts = useMemo(() => {
        const term = search.trim().toLowerCase()
        if (!term) return products
        return products.filter((product) => product.productName.toLowerCase().includes(term))
    }, [products, search])

    return (
        <DashboardLayout branchCount={branches.length} productCount={products.length} search={search} onSearchChange={setSearch}>

            <div className="page" id="dashboard">
                <section className="page-heading">
                    <div>
                    <p className="eyebrow">OVERVIEW</p>
                    <h1>Good morning, Developer.</h1>
                    <p>Here is what is happening across your branches today.</p>
                    </div>
                    <button className="primary-button" type="button" onClick={() => setIsBranchDialogOpen(true)}>
                    <Icon name="plus" size={18} /> Add branch
                    </button>
                </section>

                <DashboardStats branches={branches} />

                <BranchSection branches={filteredBranches} isLoading={isBranchesLoading} error={branchesError} search={search} onRefresh={refreshBranches} onAddBranch={() => setIsBranchDialogOpen(true)} />
                <ProductSection products={filteredProducts} isLoading={isProductsLoading} error={productsError} search={search} onRefresh={refreshProducts} onAddProduct={() => setIsProductDialogOpen(true)} />
            </div>

            {isBranchDialogOpen && (
                <CreateBranchDialog onClose={() => setIsBranchDialogOpen(false)} onCreate={addBranch} />
            )}

            {isProductDialogOpen && (
                <CreateProductDialog onClose={() => setIsProductDialogOpen(false)} onCreate={addProduct} />
            )}
            
            
        </DashboardLayout>
    )

}