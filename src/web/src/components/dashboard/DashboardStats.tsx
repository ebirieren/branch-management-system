import { useMemo } from 'react'
import type { Branch } from '../../types/branch'
import { Icon } from '../ui/Icon'
import { StatCard } from '../ui/StatCard'

type DashboardStatsProps = {
    branches: Branch[]
}

export function DashboardStats({branches}: DashboardStatsProps) {
    const totals = useMemo(() => { return branches.reduce(
        (summary, branch) => {
            summary.products += branch.products.length
            summary.stock += branch.products.reduce((sum, product) => sum + product.productCount, 0)
            summary.value += branch.products.reduce((sum, product) => sum + (product.initialProductValue ?? 0), 0)
            return summary
        },
        { products: 0, stock: 0, value: 0},
    )
}, [branches])

    return (
          <section className="stats-grid" aria-label="Branch statistics">
            <StatCard label="Total branches" value={branches.length.toString()} helper="Active locations" icon={<Icon name="warehouse" />} tone="blue" />
            <StatCard label="Product lines" value={totals.products.toLocaleString()} helper="Across all branches" icon={<Icon name="layers" />} tone="violet" />
            <StatCard label="Units in stock" value={totals.stock.toLocaleString()} helper="Current inventory" icon={<Icon name="box" />} tone="orange" />
            <StatCard label="Inventory value" value={formatCurrency(totals.value)} helper="Initial product value" icon={<Icon name="value" />} tone="green" />
          </section>
    )
}

function formatCurrency(value: number) {
  return new Intl.NumberFormat('tr-TR', {
    style: 'currency',
    currency: 'TRY',
    maximumFractionDigits: 0,
    notation: value >= 1_000_000 ? 'compact' : 'standard',
  }).format(value)
}