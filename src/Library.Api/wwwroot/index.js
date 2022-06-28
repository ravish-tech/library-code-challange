let activeBookId = null;
let searchPhrase = "";

window.addEventListener('load', () => {
    document.getElementById("search-phrase-input").onkeyup = onSearchPhraseChange;
    document.getElementById("clear-button").onclick = onClear;
    fetchAndDisplayBooks();
});

/// Event handlers
function onSearchPhraseChange() {
    searchPhrase = this.value ?? "";
    debouncedFetchWords();
}

function onBookClick(bookId, bookName) {
    if (activeBookId) document.getElementById(activeBookId).classList.remove("active");
    document.getElementById(bookId).classList.add("active");
    activeBookId = bookId;
    document.getElementById("book-title").innerText = bookName;
    fetchAndDisplayWords(bookId);
}

function onClear() {
    const searchInput = document.getElementById("search-phrase-input");
    searchInput.value = "";
    searchInput.dispatchEvent(new KeyboardEvent('keyup', {}));
}

/// Dom manipulators (Books and Words dom generators)
function displayBooks(books) {
    const bookContainer = document.getElementById("list-of-books-container");
    while (bookContainer.firstChild) bookContainer.removeChild(bookContainer.firstChild);
    for (const bookId in books) {
        const bookButton = document.createElement("button");
        bookButton.attributes["Type"] = "button";
        bookButton.textContent = books[bookId];
        bookButton.classList.add("list-group-item", "list-group-item-action")
        bookButton.id = bookId;
        bookButton.onclick = () => onBookClick(bookId, books[bookId]);
        bookContainer.appendChild(bookButton);
    }
    bookContainer.firstChild.click();
}


function displayWords(wordList) {
    const wordCountContainer = document.getElementById("word-count-table-container");
    while (wordCountContainer.firstChild) wordCountContainer.removeChild(wordCountContainer.firstChild);
    let counter = 1;
    for (const word in wordList) {
        const tableRow = document.createElement("tr");
        const counterTd = document.createElement("td");
        counterTd.textContent = (counter++).toString();
        tableRow.append(counterTd);
        const wordTd = document.createElement("td");
        wordTd.textContent = word;
        tableRow.appendChild(wordTd);
        const countTd = document.createElement("td");
        countTd.textContent = wordList[word];
        tableRow.appendChild(countTd);
        wordCountContainer.append(tableRow);
    }
}

/// Rest Calls
const debouncedFetchWords = debounce(fetchAndDisplayWords);

function fetchAndDisplayWords() {
    searchPhrase = searchPhrase.trim().toLowerCase();
    if (searchPhrase.length < 3 && searchPhrase.length > 0)
        document.getElementById("info-text").classList.add("text-danger");
    else document.getElementById("info-text").classList.remove("text-danger");
    const query = searchPhrase ? `query=${searchPhrase}` : "";
    fetch(`api/Books/${activeBookId}?${query}`)
        .then(response => response.json())
        .then(data => displayWords(data));
}

function fetchAndDisplayBooks() {
    fetch("api/Books")
        .then(response => response.json())
        .then(data => displayBooks(data));
}

/// Utility
function debounce(func, timeout = 500) {
    let timer;
    return (...args) => {
        clearTimeout(timer);
        timer = setTimeout(() => {
            func.apply(this, args);
        }, timeout);
    };
}