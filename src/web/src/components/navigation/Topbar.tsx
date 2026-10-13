import { useEffect, useRef } from "react";
import { Icon } from '../ui/Icon'

type TopbarProps = {
    search: string
    onSearchChange: (value: string) => void
}

export function Topbar({search, onSearchChange,}: TopbarProps){
    const searchInputRef = useRef<HTMLInputElement>(null)

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

    return (
        <header className="topbar">
          <div className="top-search">
            <Icon name="search" size={18} />
            <input
              ref={searchInputRef}
              aria-label="Search branches"
              placeholder="Search branches..."
              value={search}
              onChange={(event) => onSearchChange(event.target.value)}
            />
            <kbd>⌘ K</kbd>
          </div>
          <div className="profile" aria-label="Signed in user">
            <span className="avatar">ED</span>
            <span><strong>Developer</strong><small>Learning workspace</small></span>
          </div>
        </header>
    )
}