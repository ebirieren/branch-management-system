import { useEffect, useMemo, useRef, useState } from 'react'
import type { FormEvent, ReactNode } from 'react'
import { createBranch, getBranches } from './api/branches'
import { BranchCard } from './components/BranchCard'
import { StatCard } from './components/StatCard'
import type { Branch } from './types/branch'
import './App.css'

type IconName =
  | 'grid'
  | 'branch'
  | 'box'
  | 'receipt'
  | 'search'
  | 'plus'
  | 'refresh'
  | 'warehouse'
  | 'layers'
  | 'value'
  | 'close'

function Icon({ name, size = 20 }: { name: IconName; size?: number }) {
  const paths: Record<IconName, ReactNode> = {
    grid: <><rect x="3" y="3" width="7" height="7" rx="1"/><rect x="14" y="3" width="7" height="7" rx="1"/><rect x="3" y="14" width="7" height="7" rx="1"/><rect x="14" y="14" width="7" height="7" rx="1"/></>,
    branch: <><circle cx="6" cy="5" r="2"/><circle cx="18" cy="5" r="2"/><circle cx="12" cy="19" r="2"/><path d="M6 7v3c0 2 1.5 3 3 3h6c1.5 0 3-1 3-3V7M12 13v4"/></>,
    box: <><path d="m4 7 8-4 8 4-8 4-8-4Z"/><path d="m4 7 8 4 8-4v10l-8 4-8-4V7Z"/><path d="M12 11v10"/></>,
    receipt: <><path d="M6 3h12v18l-3-2-3 2-3-2-3 2V3Z"/><path d="M9 8h6M9 12h6"/></>,
    search: <><circle cx="11" cy="11" r="7"/><path d="m20 20-4-4"/></>,
    plus: <path d="M12 5v14M5 12h14"/>,
    refresh: <><path d="M20 11a8 8 0 0 0-14-5l-2 2"/><path d="M4 4v4h4M4 13a8 8 0 0 0 14 5l2-2"/><path d="M20 20v-4h-4"/></>,
    warehouse: <><path d="M3 9 12 3l9 6v12H3V9Z"/><path d="M7 21v-8h10v8M9 16h6"/></>,
    layers: <><path d="m12 3 9 5-9 5-9-5 9-5Z"/><path d="m3 12 9 5 9-5M3 16l9 5 9-5"/></>,
    value: <><circle cx="12" cy="12" r="9"/><path d="M16 8.5c-.8-.7-2-1-3.2-1-1.6 0-2.8.8-2.8 2s1 1.8 3 2.3c2 .5 3 1.1 3 2.5s-1.3 2.2-3 2.2c-1.4 0-2.8-.5-3.7-1.3M12.7 5.5v2M12.7 16.5v2"/></>,
    close: <path d="m6 6 12 12M18 6 6 18"/>,
  }

  return (
    <svg width={size} height={size} viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="1.8" strokeLinecap="round" strokeLinejoin="round" aria-hidden="true">
      {paths[name]}
    </svg>
  )
}

function App() {
  const [branches, setBranches] = useState<Branch[]>([])
  const [search, setSearch] = useState('')
  const [isLoading, setIsLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)
  const [isDialogOpen, setIsDialogOpen] = useState(false)
  const [branchName, setBranchName] = useState('')
  const [isSaving, setIsSaving] = useState(false)
  const searchInputRef = useRef<HTMLInputElement>(null)

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

  useEffect(() => {
    function focusSearch(event: KeyboardEvent) {
      if ((event.metaKey || event.ctrlKey) && event.key.toLowerCase() === 'k') {
        event.preventDefault()
        searchInputRef.current?.focus()
      }
    }

    window.addEventListener('keydown', focusSearch)
    return () => window.removeEventListener('keydown', focusSearch)
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
    <div className="app-shell">
      <aside className="sidebar">
        <a className="brand" href="#top" aria-label="Stockroom home">
          <span className="brand-mark"><Icon name="warehouse" size={22} /></span>
          <span>Stockroom</span>
        </a>

        <nav className="main-nav" aria-label="Main navigation">
          <p className="nav-label">Workspace</p>
          <a className="nav-item active" href="#dashboard"><Icon name="grid" /> Dashboard</a>
          <a className="nav-item" href="#branches"><Icon name="branch" /> Branches <span className="nav-count">{branches.length}</span></a>
          <span className="nav-item muted"><Icon name="box" /> Products <span className="soon">Soon</span></span>
          <span className="nav-item muted"><Icon name="receipt" /> Invoices <span className="soon">Soon</span></span>
        </nav>

        <div className="learning-card">
          <span className="learning-icon">TS</span>
          <div>
            <strong>Learning mode</strong>
            <p>This page is built with typed React components.</p>
          </div>
        </div>
      </aside>

      <main className="main-content" id="top">
        <header className="topbar">
          <div className="top-search">
            <Icon name="search" size={18} />
            <input
              ref={searchInputRef}
              aria-label="Search branches"
              placeholder="Search branches..."
              value={search}
              onChange={(event) => setSearch(event.target.value)}
            />
            <kbd>⌘ K</kbd>
          </div>
          <div className="profile" aria-label="Signed in user">
            <span className="avatar">ED</span>
            <span><strong>Developer</strong><small>Learning workspace</small></span>
          </div>
        </header>

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
      </main>

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
    </div>
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
