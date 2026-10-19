import { Icon } from '../ui/Icon'

type SidebarProps = {
    branchCount: number
    productCount: number
}

export function Sidebar({ branchCount, productCount }: SidebarProps) {
    return (
        <aside className="sidebar">
            <a className="brand" href="#top" aria-label="Stockroom home">
                <span className="brand-mark"><Icon name="warehouse" size={22} /></span>
                <span>Stockroom</span>
            </a>

            <nav className="main-nav" aria-label="Main navigation">
                <p className="nav-label">Workspace</p>
                <a className="nav-item active" href="#dashboard"><Icon name="grid" /> Dashboard</a>
                <a className="nav-item" href="#branches"><Icon name="branch" /> Branches <span className="nav-count">{branchCount}</span></a>
                <a className='nav-item' href="#products"><Icon name="box"/> Products <span className='nav-count'>{productCount}</span></a>
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
    )
}