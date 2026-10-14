import type { Branch } from '../../../types/branch'
import { Icon } from '../../../components/ui/Icon'
import { BranchCard } from './BranchCard'

type BranchSectionProps = {
    branches: Branch[]
    isLoading: boolean
    error: string | null
    search: string
    onRefresh: () => Promise<void>
    onAddBranch: () => void
}

export function BranchSection({branches, isLoading, error, search, onRefresh, onAddBranch,}: BranchSectionProps){
    return (
          <section className="branch-section" id="branches">
            <div className="section-heading">
              <div>
                <h2>Your branches</h2>
                <p>Live data from <code>GET /api/branches</code></p>
              </div>
              <button className="icon-button" type="button" onClick={() => void onRefresh()} disabled={isLoading} aria-label="Refresh branches">
                <Icon name="refresh" size={18} /> Refresh
              </button>
            </div>

            {error && (
              <div className="notice error-notice" role="alert">
                <div><strong>We could not reach the API.</strong><p>{error}</p></div>
                <button type="button" onClick={() => void onRefresh()}>Try again</button>
              </div>
            )}

            {isLoading ? (
              <div className="branch-grid" aria-label="Loading branches">
                {[1, 2, 3].map((item) => <div className="branch-skeleton" key={item} />)}
              </div>
            ) : branches.length > 0 ? (
              <div className="branch-grid">
                {branches.map((branch, index) => (
                  <BranchCard key={branch.id} branch={branch} accentIndex={index} />
                ))}
              </div>
            ) : (
              <div className="empty-state">
                <span><Icon name="warehouse" size={28} /></span>
                <h3>{search ? 'No matching branches' : 'No branches yet'}</h3>
                <p>{search ? 'Try another search word.' : 'Create your first branch to start managing inventory.'}</p>
                {!search && <button className="primary-button" type="button" onClick={onAddBranch}><Icon name="plus" size={18} /> Add branch</button>}
              </div>
            )}
          </section>
    )
}