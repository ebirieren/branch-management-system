import { useEffect, useMemo, useState } from 'react'
import type { FormEvent } from 'react'
import { createBranch, getBranches } from '../features/branches/api/branches'
import { BranchCard } from '../features/branches/components/BranchCard'
import { StatCard } from '../components/ui/StatCard'
import type { Branch } from '../types/branch'
import { Icon } from '../components/ui/Icon'
import { Sidebar } from '../components/navigation/Sidebar'
import { Topbar } from '../components/navigation/Topbar'
import { DashboardLayout } from '../layouts/DashboardLayout'
import './App.css'


function App() {
  const [branches, setBranches] = useState<Branch[]>([])
  const [search, setSearch] = useState('')
  const [isLoading, setIsLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)
  const [isDialogOpen, setIsDialogOpen] = useState(false)
  const [branchName, setBranchName] = useState('')
  const [isSaving, setIsSaving] = useState(false)


  async function loadBranches() {
    try {
      setIsLoading(true)
      setError(null)
      setBranches(await getBranches())
    } catch (requestError) {
      setError(requestError instanceof Error ? requestError.message : 'Something went wrong.')
    } finally {
      setIsLoading(false)
    }
  }

  useEffect(() => {
    let ignoreResult = false

    void getBranches()
      .then((data) => {
        if (!ignoreResult) setBranches(data)
      })
      .catch((requestError: unknown) => {
        if (!ignoreResult) {
          setError(requestError instanceof Error ? requestError.message : 'Something went wrong.')
        }
      })
      .finally(() => {
        if (!ignoreResult) setIsLoading(false)
      })

    return () => {
      ignoreResult = true
    }
  }, [])


  const filteredBranches = useMemo(() => {
    const term = search.trim().toLowerCase()
    if (!term) return branches
    return branches.filter((branch) => branch.branchName.toLowerCase().includes(term))
  }, [branches, search])

  const totals = useMemo(() => {
    return branches.reduce(
      (summary, branch) => {
        summary.products += branch.products.length
        summary.stock += branch.products.reduce((sum, product) => sum + product.productCount, 0)
        summary.value += branch.products.reduce((sum, product) => sum + (product.initialProductValue ?? 0), 0)
        return summary
      },
      { products: 0, stock: 0, value: 0 },
    )
  }, [branches])

  async function handleCreateBranch(event: FormEvent<HTMLFormElement>) {
    event.preventDefault()
    const cleanName = branchName.trim()
    if (!cleanName) return

    try {
      setIsSaving(true)
      const newBranch = await createBranch(cleanName)
      setBranches((currentBranches) => [newBranch, ...currentBranches])
      setBranchName('')
      setIsDialogOpen(false)
    } catch (requestError) {
      setError(requestError instanceof Error ? requestError.message : 'Could not create the branch.')
    } finally {
      setIsSaving(false)
    }
  }

  return (
    <DashboardLayout branchCount={branches.length} search={search} onSearchChange={setSearch}>

        <div className="page" id="dashboard">
          <section className="page-heading">
            <div>
              <p className="eyebrow">OVERVIEW</p>
              <h1>Good morning, Developer.</h1>
              <p>Here is what is happening across your branches today.</p>
            </div>
            <button className="primary-button" type="button" onClick={() => setIsDialogOpen(true)}>
              <Icon name="plus" size={18} /> Add branch
            </button>
          </section>

          <section className="stats-grid" aria-label="Branch statistics">
            <StatCard label="Total branches" value={branches.length.toString()} helper="Active locations" icon={<Icon name="warehouse" />} tone="blue" />
            <StatCard label="Product lines" value={totals.products.toLocaleString()} helper="Across all branches" icon={<Icon name="layers" />} tone="violet" />
            <StatCard label="Units in stock" value={totals.stock.toLocaleString()} helper="Current inventory" icon={<Icon name="box" />} tone="orange" />
            <StatCard label="Inventory value" value={formatCurrency(totals.value)} helper="Initial product value" icon={<Icon name="value" />} tone="green" />
          </section>

          <section className="branch-section" id="branches">
            <div className="section-heading">
              <div>
                <h2>Your branches</h2>
                <p>Live data from <code>GET /api/branches</code></p>
              </div>
              <button className="icon-button" type="button" onClick={() => void loadBranches()} disabled={isLoading} aria-label="Refresh branches">
                <Icon name="refresh" size={18} /> Refresh
              </button>
            </div>

            {error && (
              <div className="notice error-notice" role="alert">
                <div><strong>We could not reach the API.</strong><p>{error}</p></div>
                <button type="button" onClick={() => void loadBranches()}>Try again</button>
              </div>
            )}

            {isLoading ? (
              <div className="branch-grid" aria-label="Loading branches">
                {[1, 2, 3].map((item) => <div className="branch-skeleton" key={item} />)}
              </div>
            ) : filteredBranches.length > 0 ? (
              <div className="branch-grid">
                {filteredBranches.map((branch, index) => (
                  <BranchCard key={branch.id} branch={branch} accentIndex={index} />
                ))}
              </div>
            ) : (
              <div className="empty-state">
                <span><Icon name="warehouse" size={28} /></span>
                <h3>{search ? 'No matching branches' : 'No branches yet'}</h3>
                <p>{search ? 'Try another search word.' : 'Create your first branch to start managing inventory.'}</p>
                {!search && <button className="primary-button" type="button" onClick={() => setIsDialogOpen(true)}><Icon name="plus" size={18} /> Add branch</button>}
              </div>
            )}
          </section>
        </div>

      {isDialogOpen && (
        <div className="dialog-backdrop" role="presentation" onMouseDown={() => setIsDialogOpen(false)}>
          <section className="dialog" role="dialog" aria-modal="true" aria-labelledby="dialog-title" onMouseDown={(event) => event.stopPropagation()}>
            <button className="dialog-close" type="button" onClick={() => setIsDialogOpen(false)} aria-label="Close dialog"><Icon name="close" /></button>
            <span className="dialog-icon"><Icon name="warehouse" /></span>
            <p className="eyebrow">NEW LOCATION</p>
            <h2 id="dialog-title">Add a branch</h2>
            <p>Give the new branch a clear, recognizable name.</p>
            <form onSubmit={handleCreateBranch}>
              <label htmlFor="branch-name">Branch name</label>
              <input id="branch-name" autoFocus placeholder="Example: Kadıköy Branch" value={branchName} onChange={(event) => setBranchName(event.target.value)} />
              <div className="dialog-actions">
                <button className="secondary-button" type="button" onClick={() => setIsDialogOpen(false)}>Cancel</button>
                <button className="primary-button" type="submit" disabled={isSaving || !branchName.trim()}>{isSaving ? 'Creating...' : 'Create branch'}</button>
              </div>
            </form>
          </section>
        </div>
      )}
    </DashboardLayout>
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

export default App
