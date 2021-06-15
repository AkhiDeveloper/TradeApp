import pandas as pd
from scipy import sparse

u_id

ratings = pd.read_csv('dataset/product_rating.csv')
products = pd.read_csv('dataset/products.csv')

ratings = pd.merge(products,ratings).drop(['category','timestamp'],axis=1)

filter = ratings["userId"] == 1
ratings.where(filter, inplace = True)
ratings.dropna(subset = ["rating"], inplace=True)
ratings.drop(['name','userId'], axis=1)
subset = ratings[['productId', 'rating']]
u_rating = [tuple(x) for x in subset.to_numpy()]

def get_similar(name,rating):
    similar_ratings = corrMatrix[name]*(rating-2.5)
    similar_ratings = similar_ratings.sort_values(ascending=False)
    return similar_ratings

userRatings = ratings.pivot_table(index=['userId'],columns=['productId'],values='rating')
userRatings = userRatings.dropna(thresh=10, axis=1).fillna(0,axis=1)

corrMatrix = userRatings.corr(method='pearson')

similar = pd.DataFrame()

for product,rating in u_rating:
    similar = similar.append(get_similar(product,rating),ignore_index = True)

output = similar.sum().sort_values(ascending=False).head(100)
