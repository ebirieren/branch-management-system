import type { CSSProperties } from 'react'
import type { Branch } from '../../../types/branch'

type BranchCardProps = {
  branch: Branch
  accentIndex: number
}

const accentColors = ['#2478f0', '#7659d6', '#e7782b', '#1b9c69']

export function BranchCard({ branch, accentIndex }: BranchCardProps) {
  const units = branch.products.reduce((sum, product) => sum + product.productCount, 0)
  const initials = branch.branchName
    .split(' ')
    .slice(0, 2)
    .map((word) => word[0])
    .join('')
    .toUpperCase()

  return (
    <article className="branch-card" style={{ '--branch-accent': accentColors[accentIndex % accentColors.length] } as CSSProperties}>
      <div className="branch-card-header">
        <span className="branch-initials">{initials || 'BR'}</span>
        <div>
          <h3>{branch.branchName}</h3>
          <span className="branch-status">Active branch</span>
        </div>
      </div>
      <div className="branch-metrics">
        <div><span>Product lines</span><strong>{branch.products.length}</strong></div>
        <div><span>Units in stock</span><strong>{units.toLocaleString()}</strong></div>
      </div>
      <footer className="branch-footer">
        <span>Created {formatDate(branch.createdAt)}</span>
        <span className="branch-link">Branch #{branch.id}</span>
      </footer>
    </article>
  )
}

function formatDate(date: string) {
  return new Intl.DateTimeFormat('en', { month: 'short', day: 'numeric', year: 'numeric' }).format(new Date(date))
}
