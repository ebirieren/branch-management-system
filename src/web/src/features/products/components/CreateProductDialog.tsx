import { useState } from "react"
import type { FormEvent } from "react"
import { Icon } from "../../../components/ui/Icon"

type CreateProductDialogProps = {
    onClose: () => void
    onCreate: (productName: string, productPrice: number, productCount: number, supplierName: string, taxRate: number) => Promise<boolean>
}

export function CreateProductDialog({onClose, onCreate}: CreateProductDialogProps){
    const [productName, setProductName] = useState('')
    const [productPrice, setProductPrice] = useState('')
    const [productCount, setProductCount] = useState('')
    const [supplierName, setSupplierName] = useState('')
    const [taxRate, setTaxRate]  = useState('')
    const [isSaving, setIsSaving] = useState(false)

    async function handleSubmit(event: FormEvent<HTMLFormElement>) {
        event.preventDefault()

        const cleanName = productName.trim()
        const cleanSupplier = supplierName.trim()

        const parsedPrice = Number(productPrice)
        const parsedCount = Number(productCount)
        const parsedTax = Number(taxRate)

        if (!cleanName || !cleanSupplier) {
            return
        }

        if(!Number.isFinite(parsedPrice) || parsedPrice<0){
            return
        }

        if(!Number.isInteger(parsedCount) || parsedCount<0){
            return
        }

        if(!Number.isFinite(parsedTax) || parsedTax<0 || parsedTax > 100){
            return
        }

        try{
            setIsSaving(true)

            const wasCreated = await onCreate(cleanName, parsedPrice, parsedCount, cleanSupplier, parsedTax)

            if (wasCreated){
                onClose()
            }
        } finally{
            setIsSaving(false)
        }
    }

    return (
        <div className="dialog-backdrop" role="presentation" onMouseDown={onClose}>
            <section className="dialog" role="dialog" aria-modal="true" aria-labeledby="dialog-title" onMouseDown={(event) => event.stopPropagation()}>
                <button className="dialog-close" type="button" onClick={onClose} aria-label="Close dialog"><Icon name="close" /></button>
                <span className="dialog-icon"><Icon name="box" /></span>
                <p className="eyebrow">NEW PRODUCT</p>
                <h2 id="dialog-title">Add a product</h2>
                <p>Give the new product a clear, recognizable name</p>
                <form onSubmit={handleSubmit}>
                    <label htmlFor="product-name">Product name</label>
                    <input id="product-name" autoFocus placeholder="Example: Paradontax Diş Macunu" value={productName} onChange={(event) => setProductName(event.target.value)} />

                    <label htmlFor="product-price">Product price</label>
                    <input id="product-price" type="number" min="0" step="0.01" autoFocus value={productPrice} onChange={(event) => setProductPrice(event.target.value)} />

                    <label htmlFor="product-count">Product count</label>
                    <input id="product-count" type="number" min="0" step="1" autoFocus value={productCount} onChange={(event) => setProductCount(event.target.value)} />

                    <label htmlFor="supplier-name">Supplier name</label>
                    <input id="supplier-name" autoFocus value={supplierName} onChange={(event) => setSupplierName(event.target.value)} />

                    <label htmlFor="tax-rate">Tax rate</label>
                    <input id="tax-rate" type="number" min="0" max="100" step="1"autoFocus value={taxRate} onChange={(event) => setTaxRate(event.target.value)} />
                </form>
                <p></p>
            </section>
        </div>
    )
}